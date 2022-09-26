import { useState } from "react";

import authService from "../../services/authService";
import {
  Grid,
  TextField,
  Paper,
  Button,
  InputAdornment,
  IconButton,
} from "@mui/material";
import VisibilityIcon from "@mui/icons-material/Visibility";
import VisibilityOffIcon from "@mui/icons-material/VisibilityOff";

import { capitalizeFirstLetter } from "../../utils/string-utils";

const claimTypes = ["username", "email"];
const constraints = {
  [claimTypes[0]]: {
    minLength: 5,
    maxLength: 20,
  },
  [claimTypes[1]]: {
    minLength: 10,
    maxLength: 50,
  },
  password: {
    maxLength: 24,
  },
};

export default function LoginForm() {
  const [userClaim, setUserClaim] = useState({
    value: "",
    type: claimTypes[0],
    error: "",
  });
  const [password, setPassword] = useState({
    value: "",
    isShown: false,
  });

  // Action: this function checks whether the claim is
  // a username or an email base on character "@".
  const handleChangeClaim = (e) => {
    let {
      currentTarget: { value },
    } = e;

    value = value.replace(/\s/g, "");

    if (value.includes("@")) {
      setUserClaim({
        value,
        type: claimTypes[1],
        error: getUserClaimErrorMessage(value, claimTypes[1]),
      });
    } else {
      setUserClaim({
        value,
        type: claimTypes[0],
        error: getUserClaimErrorMessage(value, claimTypes[0]),
      });
    }
  };

  // Action: this function prevent space character,
  // it doesn't handle other special characters yet
  const handleChangePassword = (e) => {
    let {
      currentTarget: { value },
    } = e;

    value = value.replace(/\s/g, "");

    if (value.length <= constraints.password.maxLength) {
      setPassword({ value, isShown: password.isShown });
    }
  };

  const handleClickShowPassword = () => {
    setPassword({ value: password.value, isShown: !password.isShown });
  };

  const handleMouseDownPassword = () => {
    setPassword({ value: password.value, isShown: !password.isShown });
  };

  const handleSubmit = (e) => {
    // Prevent default submitting action (go to subfix "?" url).
    e.preventDefault();

    loginAsync();
  };

  const loginAsync = async () => {
    try {
      if (userClaim.type === claimTypes[0]) {
        await authService.loginAsync(userClaim.value, null, password.value);
      } else {
        await authService.loginAsync(null, userClaim.value, password.value);
      }
    } catch (error) {
      if (error.response && error.response.status === 400) {
        const { data: errorMessage } = error.response;
        setUserClaim({
          value: userClaim.value,
          type: userClaim.type,
          error: errorMessage,
        });
      }
    }
  };

  const isSubmitable = () => {
    return isValidUserClaim(userClaim.value, userClaim.type);
  };

  const isValidUserClaim = (value, claimType) => {
    return (
      value.length >= constraints[claimType].minLength &&
      value.length <= constraints[claimType].maxLength
    );
  };

  const getUserClaimErrorMessage = (value, claimType) => {
    if (!isValidUserClaim(value, claimType)) {
      const capUserClaim = capitalizeFirstLetter(claimType);

      return `${capUserClaim} length must be between ${constraints[claimType].minLength} and ${constraints[claimType].maxLength}.`;
    }

    return "";
  };

  const paperStyle = {
    padding: "20px",
    height: "24vh",
    width: 300,
    margin: "20px auto",
    "border-radius": "10px",
  };

  return (
    <Grid>
      <Paper elevation={10} sx={paperStyle}>
        <TextField
          sx={{ margin: "10px auto" }}
          value={userClaim.value}
          placeholder="Username or email"
          //type={userClaim.type === claimType[1] ? "email" : "text"}
          onChange={handleChangeClaim}
          error={Boolean(userClaim.error)}
          helperText={userClaim.error}
          fullWidth
          required
        />
        <TextField
          sx={{ margin: "10px auto" }}
          value={password.value}
          placeholder="Password"
          type={password.isShown ? "text" : "password"}
          onChange={handleChangePassword}
          InputProps={{
            // Toggle button.
            endAdornment: (
              <InputAdornment position="end">
                <IconButton
                  aria-label="toggle password visibility"
                  onClick={handleClickShowPassword}
                  onMouseDown={handleMouseDownPassword}
                >
                  {password.isShown ? (
                    <VisibilityIcon />
                  ) : (
                    <VisibilityOffIcon />
                  )}
                </IconButton>
              </InputAdornment>
            ),
          }}
          fullWidth
          required
        />
        <Button
          color="primary"
          type="submit"
          disabled={!isSubmitable()}
          onClick={handleSubmit}
          fullWidth
          required
        >
          Login
        </Button>
      </Paper>
    </Grid>
  );
}
