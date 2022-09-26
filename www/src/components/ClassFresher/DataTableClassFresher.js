import axios from "axios";
import React, { useState } from "react";
import { useHistory } from "react-router-dom";
import { configEnv } from "../../configs/config";
import BorderColorIcon from "@mui/icons-material/BorderColor";
import AccountCircleIcon from "@mui/icons-material/AccountCircle";
import DeleteOutlineIcon from "@mui/icons-material/DeleteOutline";

const TABLE_HEAD = [
  { id: "classCode", label: "Class Code", alignRight: false },
  { id: "isDone", label: "Status", alignRight: false },
  { id: "startDate", label: "Start Date", alignRight: false },
  { id: "endDate", label: "End Date", alignRight: false },
  { id: "RRCode", label: "RRCode", alignRight: false },
];

export default function DataTableClassFresher({ classFresher }) {
  const [isOpenNoti, setIsOpenNoti] = useState(false);
  const history = useHistory();
  const [message, setMessage] = useState("");
  const [idClass, setIdClass] = useState();
  const [isOpen, setIsOpen] = useState(false);

  const GotoDetail = (e) => {
    history.push(`classes/detail/${e}`, { state: { id: e } });
  };
  const GotoEdit = (e) => {
    history.push(`classes/edit/${e}`, { state: { id: e } });
  };
  const CloseModal = () => {
    setIsOpen(false);
  };
  const OpenModal = (data) => {
    setMessage(`Do you want to delete class ${data.classCode} !!`);
    setIdClass(data.id);
    setIsOpen(true);
  };
  const DeleteClass = () => {
    axios
      .delete(
        `${configEnv.FETCH_STRING}ClassFresher/DeleteClassFresher?classFresherId=${idClass}`
      )
      .then((res) => {
        if (res.data) {
          setIsOpenNoti(true);
          window.location.reload();
          setTimeout(() => {
            setIsOpenNoti(false);
          }, 2000);
        }
      });
  };

  return (
    <div>
      {isOpenNoti ? (
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
              Delete success !!!
            </div>
          </div>
        </div>
      ) : null}
      <div
        class={isOpen ? "modal fade show" : "modal fade"}
        id="exampleModal"
        tabindex="-1"
        role="dialog"
        aria-labelledby="exampleModalLabel"
        aria-hidden="false"
        style={
          isOpen
            ? { display: "block", backgroundColor: "rgb(0,0,0,0.5)" }
            : { display: "none" }
        }
      >
        <div class="modal-dialog" role="document">
          <div class="modal-content">
            <div class="modal-header">
              <h5 class="modal-title" id="exampleModalLabel">
                Delete Class
              </h5>
              <button
                onClick={CloseModal}
                type="button"
                class="close"
                data-dismiss="modal"
                aria-label="Close"
              >
                <span aria-hidden="true">&times;</span>
              </button>
            </div>
            <div class="modal-body">{message}</div>
            <div class="modal-footer">
              <button
                onClick={CloseModal}
                type="button"
                class="btn btn-danger"
                data-dismiss="modal"
              >
                Cancel
              </button>
              <button
                onClick={() => DeleteClass()}
                type="button"
                class="btn btn-success"
                data-dismiss="modal"
              >
                Ok
              </button>
            </div>
          </div>
        </div>
      </div>
      <table class="table table-hover table-bordered table-striped">
        <thead
          class="thead-blue"
          style={{ backgroundColor: "#28a745", color: "white" }}
        >
          <tr>
            {TABLE_HEAD.map((header) => {
              return <th scope="col">{header.label}</th>;
            })}
            <th>Option</th>
          </tr>
        </thead>
        <tbody>
          {classFresher.map((element) => {
            return (
              <tr>
                <td>{element.classCode}</td>
                <td>
                  {element.status ? (
                    <button
                      class="btn btn-success"
                      style={{ minWidth: "100px" }}
                    >
                      Done
                    </button>
                  ) : (
                    <button
                      class="btn btn-warning"
                      style={{ minWidth: "100px" }}
                    >
                      Process
                    </button>
                  )}
                </td>
                <td>{element.startDate.slice(0, 10)}</td>
                <td>{element.endDate.slice(0, 10)}</td>
                <td>{element.rrCode}</td>
                <tr>
                  <button
                    className="btn btn-success"
                    onClick={() => GotoEdit(element.id)}
                  >
                    <BorderColorIcon />
                  </button>
                  &nbsp; &nbsp; &nbsp; &nbsp;
                  <button
                    className="btn btn-warning"
                    onClick={() => GotoDetail(element.id)}
                  >
                    <AccountCircleIcon />
                  </button>
                  &nbsp; &nbsp; &nbsp; &nbsp;
                  <button
                    className="btn btn-danger"
                    onClick={() => OpenModal(element)}
                  >
                    <DeleteOutlineIcon />
                  </button>
                </tr>
              </tr>
            );
          })}
        </tbody>
      </table>
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
