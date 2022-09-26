using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System;
using System.Security.Cryptography;

namespace Global.Shared.Helpers
{
    public class PasswordHasher
    {
        // Use Pascal convention for const in C#
        private const byte BitSize = 8;

        // Use 128-bit salt (we commonly use 16 bytes salt)
        private const byte SaltBit = 128;

        private const byte SaltSize = SaltBit / BitSize;

        // Number of salting times
        private const ushort IterationCount = 10000;

        // Specify the HMAC to be used
        private const KeyDerivationPrf Prf = KeyDerivationPrf.HMACSHA256;

        // The desired length (in bits) of the derived key
        // is the same as the output length of HMACSHA256
        private const ushort DesiredKeyLengthBit = 256;

        private const byte SubKeySize = DesiredKeyLengthBit / BitSize;

        private const byte MarkerTSize = sizeof(byte);
        private const byte PrfTSize = sizeof(uint);
        private const byte IterCountTSize = sizeof(uint);
        private const byte SaltTSize = sizeof(uint);

        private const byte PrfOffset = MarkerTSize;
        private const byte IterCountOffset = PrfOffset + PrfTSize;
        private const byte SaltSizeOffset = IterCountOffset + IterCountTSize;
        private const byte SaltOffset = SaltSizeOffset + SaltTSize;
        private const byte SubKeyOffset = SaltOffset + SaltSize;

        public static string HashPassword(string password)
        {
            if (string.IsNullOrWhiteSpace(password))
                throw new ArgumentNullException(nameof(password));

            var hashedPassword = Convert.ToBase64String(GenerateHashedPassword(password));

            return hashedPassword;
        }

        public static PasswordVerificationResult Compare(string providedPassword, string hashedPassword)
        {
            if (providedPassword == null)
                throw new ArgumentNullException(nameof(providedPassword));

            if (hashedPassword == null)
                throw new ArgumentNullException(nameof(hashedPassword));

            byte[] decodedHashedPassword = Convert.FromBase64String(hashedPassword);

            // Read the format marker from the hashed password
            // Hashed password must be at least 4 bytes
            if (decodedHashedPassword.Length == 0)
                return PasswordVerificationResult.Failed;

            if (decodedHashedPassword[0] != 0x01 || !Compare(providedPassword, decodedHashedPassword))
                return PasswordVerificationResult.Failed;

            return PasswordVerificationResult.Success;
        }

        private static bool Compare(string providedPassword, byte[] hashedPassword)
        {
            try
            {
                // Read header information
                var prf = (KeyDerivationPrf) ReadNetworkByteOrder(hashedPassword, PrfOffset);
                var iterCount = (ushort) ReadNetworkByteOrder(hashedPassword, IterCountOffset); // this should always equal to IterationCount
                int saltSize = (int) ReadNetworkByteOrder(hashedPassword, SaltSizeOffset);

                // Retrieved salt size must be 128 bits
                if (saltSize != SaltSize)
                    return false;

                byte[] salt = new byte[SaltSize];
                Buffer.BlockCopy(hashedPassword, SaltOffset, salt, 0, SaltSize);

                // Retrieved subkey (the rest of the payload) must be 256 bits
                int subkeyLength = hashedPassword.Length - SubKeyOffset;
                if (subkeyLength != SubKeySize)
                    return false;

                byte[] expectedSubkey = new byte[SubKeySize];
                Buffer.BlockCopy(hashedPassword, SubKeyOffset, expectedSubkey, 0, SubKeySize);

                // Hash the incoming password and verify it
                byte[] actualSubkey = KeyDerivation.Pbkdf2(
                                        password: providedPassword,
                                        salt: salt,
                                        prf: prf,
                                        iterationCount: iterCount,
                                        numBytesRequested: SubKeySize);

                return CryptographicOperations.FixedTimeEquals(actualSubkey, expectedSubkey);
            }
            catch
            {
                // This should never occur except in the case of a malformed payload, where
                // we might go off the end of the array. Regardless, a malformed payload
                // implies verification failed.
                return false;
            }
        }

        private static byte[] GenerateHashedPassword(string password)
        {
            var salt = new byte[SaltSize];

            // Generates a 128-bit salt using a cryptographically
            // strong random sequence of nonzero values
            using (var rngCsp = RandomNumberGenerator.Create())
                rngCsp.GetNonZeroBytes(salt);

            // Derives a 256-bit subkey using HMACSHA256
            var subKey = KeyDerivation.Pbkdf2(
                                password: password,
                                salt: salt,
                                prf: Prf,
                                iterationCount: IterationCount,
                                numBytesRequested: SubKeySize);

            // Structure: M: Format marker, P: Prf, I: IterCount, S: SaltSize, s: salt, k: SubKey
            // M P P P P I I I I S S S S s s s s s s s s s s s s s s s s k k k k k k k k k k k k k k k k k k k k k k k k k k k k k k k k k k k k k k k k k k k k k k k k k k k k k k k k k k k k k k k k
            // M P*4 I*4 S*4 s*16 k*64
            var outputBytes = new byte[SubKeyOffset + SubKeySize];
            outputBytes[0] = 0x01; // Format marker

            WriteNetworkByteOrder(outputBytes, PrfOffset, (uint) Prf);
            WriteNetworkByteOrder(outputBytes, IterCountOffset, IterationCount);
            WriteNetworkByteOrder(outputBytes, SaltSizeOffset, SaltSize);

            Buffer.BlockCopy(salt, 0, outputBytes, SaltOffset, SaltSize);
            Buffer.BlockCopy(subKey, 0, outputBytes, SubKeyOffset, SubKeySize);

            return outputBytes;
        }

        private static void WriteNetworkByteOrder(byte[] buffer, int offset, uint value)
        {
            var size = sizeof(uint);
            for (byte i = 0; i < size; i++)
                buffer[offset + i] = (byte) (value >> ((size - 1 - i) * BitSize));
        }

        private static uint ReadNetworkByteOrder(byte[] buffer, int offset)
        {
            var size = sizeof(uint);
            uint value = 0;

            for (byte i = 0; i < size; i++)
                value |= (uint) (buffer[offset + i]) << ((size - 1 - i) * BitSize);

            return value;
        }
    }
}
