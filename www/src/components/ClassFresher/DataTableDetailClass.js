import React, { useEffect, useState } from "react";
import { FresherReportStatus } from "../../configs/enums";
import CancelIcon from "@mui/icons-material/Cancel";
import ThumbUpOffAltIcon from "@mui/icons-material/ThumbUpOffAlt";
import axios from "axios";
import { configEnv } from "../../configs/config";

const TABLE_HEAD = [
  { id: "accountName", label: "Account", alignRight: false },
  { id: "fullName", label: "Full Name", alignRight: false },
  { id: "email", label: "Email", alignRight: false },
  { id: "dob", label: "DoB", alignRight: false },
  { id: "english", label: "English", alignRight: false },
  { id: "status", label: "Status", alignRight: false },
  { id: "phone", label: "Phone", alignRight: false },
];

export default function DataTableDetailClass({ freshers, isEdit }) {
  const [message, setMessage] = useState();
  const [isOpen, setIsOpen] = useState();

  let listStatus = [];
  const ChangeStatusFresher = (e, id) => {
    console.log(e);
    var temp = listStatus.filter(function (el) {
      return el.id != id;
    });
    temp.push({ id: id, status: parseInt(e) });
    listStatus = temp;
  };

  const ConfirmStatus = () => {
    console.log(listStatus);
    axios
      .put(`${configEnv.FETCH_STRING}Fresher/ChangStatusFresher`, listStatus)
      .then((res) => {
        setIsOpen(true);
        setMessage(res.data);
        setTimeout(() => {
          setIsOpen(false);
        }, 2000);
      });
  };

  return (
    <div>
      {isOpen ? (
        <div
          class="position-fixed top-0 end-0 p-3"
          style={{ zIndex: "99999", top: "0", right: "0", color: "white" }}
        >
          <div
            class="toast fade show"
            role="alert"
            style={{ backgroundColor: "green" }}
          >
            <div class="toast-body" style={{ fontSize: "20px" }}>
              {message}
            </div>
          </div>
        </div>
      ) : null}
      <table class="table table-hover table-bordered table-striped">
        <thead
          class="thead-blue"
          style={{ backgroundColor: "#28a745", color: "white" }}
        >
          <tr>
            {TABLE_HEAD.map((header) => {
              return (
                <th key={header.id} scope="col">
                  {header.label}
                </th>
              );
            })}
          </tr>
        </thead>
        <tbody>
          {freshers.map((fresher) => {
            return (
              <tr>
                <td>{fresher.accountName}</td>
                <td>
                  {fresher.lastName} {fresher.firstName}
                </td>
                <td>{fresher.email}</td>
                <td>{fresher.dob.slice(0, 10)}</td>
                <td>{fresher.english}</td>
                {isEdit ? (
                  <select
                    style={{
                      padding: "5px",
                      width: "138px",
                      borderRadius: "3px",
                    }}
                    onChange={(e) =>
                      ChangeStatusFresher(e.target.value, fresher.id)
                    }
                  >
                    <option value="" selected disabled hidden>
                      {fresher.status}
                    </option>
                    <option value={0}>Active</option>
                    <option value={1}>Drop Out</option>
                    <option value={2}>Failed</option>
                    <option value={3}>Passed</option>
                  </select>
                ) : (
                  <td>
                    {fresher.status === "Active" ? (
                      <button
                        class="btn btn-success"
                        style={{ minWidth: "90px" }}
                      >
                        {fresher.status}
                      </button>
                    ) : fresher.status === "DropOut" ? (
                      <button
                        class="btn btn-danger"
                        style={{ minWidth: "90px" }}
                      >
                        {fresher.status}
                      </button>
                    ) : (
                      <button
                        class="btn btn-warning"
                        style={{ minWidth: "90px" }}
                      >
                        {fresher.status}
                      </button>
                    )}
                  </td>
                )}
                <td>{fresher.phone}</td>
              </tr>
            );
          })}
        </tbody>
      </table>
      {isEdit ? (
        <div style={{ display: "flex", justifyContent: "justify-content" }}>
          <button
            onClick={ConfirmStatus}
            type="button"
            class="btn btn-success"
            style={{ margin: "12px" }}
          >
            <ThumbUpOffAltIcon /> &nbsp; Confirm Status
          </button>
        </div>
      ) : null}

      <nav aria-label="Page navigation example">
        <ul class="pagination justify-content-end">
          <li class="page-item disabled">
            <a class="page-link" href="#" tabindex="-1">
              Previous
            </a>
          </li>
          <li class="page-item">
            <a class="page-link" href="#">
              1
            </a>
          </li>
          <li class="page-item">
            <a class="page-link" href="#">
              2
            </a>
          </li>
          <li class="page-item">
            <a class="page-link" href="#">
              3
            </a>
          </li>
          <li class="page-item">
            <a class="page-link" href="#">
              Next
            </a>
          </li>
        </ul>
      </nav>
    </div>
  );
}
