import React, { useState } from "react";
import { Snackbar, Alert } from "@mui/material";
import axios from "axios";
import { configEnv } from "../../configs/config";
import DataTableClassFresher from "../../components/ClassFresher/DataTableDetailClass";
import SaveIcon from "@mui/icons-material/Save";
////////////////////////////////////

export default function DataTableImportClass({ classFresher }) {
  const [isOpen, setIsopen] = useState(false);
  const [message, setMessage] = useState();
  const ShowListFresher = () => {
    if (isOpen) setIsopen(false);
    else setIsopen(true);
  };

  const [isOpenClass, setIsopenClasss] = useState(false);

  const [classInfo, setClassInfo] = useState(classFresher);
  const [toast, setToast] = useState(false);
  const handleClose = () => {
    setToast(false);
  };
  const SeeDetailClass = () => {
    setIsopenClasss((pre) => !pre);
  };

  const ConfirmClassFresher = () => {
    var guid = broofa();
    setClassInfo({ ...classInfo, planId: guid });
    console.log(classInfo);
    axios
      .put(
        `${configEnv.FETCH_STRING}ClassFresher/UpdateClassFresherInfomation`,
        classInfo
      )
      .then((res) => {
        setMessage(res.data);
        if (res.status === 200) {
          setToast(true);
          setTimeout(() => {
            setToast(false);
          }, 1500);
        }
      });
  };
  function broofa() {
    return "xxxxxxxx-xxxx-4xxx-yxxx-xxxxxxxxxxxx".replace(
      /[xy]/g,
      function (c) {
        var r = (Math.random() * 16) | 0,
          v = c == "x" ? r : (r & 0x3) | 0x8;
        return v.toString(16);
      }
    );
  }
  function ConvertDateTime(date) {
    var yourdate = date.split("-").reverse();
    var tmp = yourdate[1];
    yourdate[0] = yourdate[1];
    yourdate[1] = tmp;
    yourdate = yourdate.join("/");
    return yourdate;
  }

  return (
    <div>
      <Snackbar open={toast} autoHideDuration={3000} onClick={handleClose}>
        <Alert severity="success" sx={{ width: "100%" }}>
          Confirm Class {classFresher.classCode} success !!
        </Alert>
      </Snackbar>
      <div class="card">
        <div style={{ width: "100%", padding: "20px" }}>
          <div class="alert alert-success" role="alert">
            <h5>{classFresher.classCode}</h5>
          </div>
          <div class="container" style={{ display: "contents" }}>
            <div class="row">
              <div class="col-md-4">
                <h5>Total Fresher : {classFresher.freshers.length}</h5>
              </div>
              <div class="col-md-4">
                <h5>RR Code: {classFresher.rrCode}</h5>
              </div>
            </div>
          </div>
          <button onClick={SeeDetailClass} class="btn btn-warning">
            Show Detail Class
          </button>
          {isOpenClass ? (
            <div>
              <div>
                <h4>Class Fresher</h4>
                <div style={{ display: "flex" }}>
                  <div class="row" style={{ width: "100%" }}>
                    <div class="col-sm-6 col-xl-4">
                      <div class="card">
                        <div class="card-body" style={{ padding: "10px 20px" }}>
                          <div>
                            <form class="outer-repeater">
                              <div class="mb-3">
                                <label htmlFor="formname" class="form-label">
                                  Start Date :
                                </label>
                                <input
                                  defaultValue={classFresher.nameAdmin1}
                                  required
                                  id="formname"
                                  placeholder="Enter your Name..."
                                  type="date"
                                  class="form-control form-control"
                                  onChange={(e) =>
                                    setClassInfo({
                                      ...classInfo,
                                      startDate: ConvertDateTime(
                                        e.target.value
                                      ),
                                    })
                                  }
                                />
                              </div>
                            </form>
                          </div>
                        </div>
                      </div>
                    </div>
                    <div class="col-sm-6 col-xl-4">
                      <div class="card">
                        <div class="card-body" style={{ padding: "10px 20px" }}>
                          <div>
                            <form class="outer-repeater">
                              <div class="mb-3">
                                <label htmlFor="formname" class="form-label">
                                  End Date :
                                </label>
                                <input
                                  defaultValue={classFresher.nameAdmin1}
                                  id="formname"
                                  required
                                  placeholder="Enter your Name..."
                                  type="date"
                                  class="form-control form-control"
                                  onChange={(e) =>
                                    setClassInfo({
                                      ...classInfo,
                                      endDate: ConvertDateTime(e.target.value),
                                    })
                                  }
                                />
                              </div>
                            </form>
                          </div>
                        </div>
                      </div>
                    </div>
                    <div class="col-sm-6 col-xl-4">
                      <div class="card">
                        <div class="card-body" style={{ padding: "10px 20px" }}>
                          <form class="outer-repeater">
                            <div class="mb-3">
                              <label htmlFor="formname" class="form-label">
                                Plan :
                              </label>
                              <select
                                style={{
                                  height: "38px",
                                  padding: "5px",
                                  width: "100%",
                                  borderRadius: "3px",
                                }}
                              >
                                <option
                                  value=""
                                  selected
                                  disabled
                                  hidden
                                ></option>
                                <option value={0}>Name Plan 1</option>
                                <option value={1}>Name Plan 2</option>
                                <option value={2}>Name Plan 3</option>
                                <option value={3}>Name Plan 4</option>
                              </select>
                            </div>
                          </form>
                        </div>
                      </div>
                    </div>
                  </div>
                </div>
                <h4>Admin</h4>
                <div style={{ display: "flex" }}>
                  <div class="row" style={{ width: "100%" }}>
                    <div class="col-sm-6 col-xl-4">
                      <div class="card">
                        <div class="card-body" style={{ padding: "10px 20px" }}>
                          <div>
                            <form class="outer-repeater">
                              <div class="mb-3">
                                <label htmlFor="formname" class="form-label">
                                  Name :
                                </label>
                                <input
                                  defaultValue={classFresher.nameAdmin1}
                                  id="formname"
                                  placeholder="Enter your Name..."
                                  type="text"
                                  class="form-control form-control"
                                  onChange={(e) =>
                                    setClassInfo({
                                      ...classInfo,
                                      nameAdmin1: e.target.value,
                                    })
                                  }
                                />
                              </div>
                              <div class="mb-3">
                                <label htmlFor="formemail" class="form-label">
                                  Email :
                                </label>
                                <input
                                  onChange={(e) =>
                                    setClassInfo({
                                      ...classInfo,
                                      emailAdmin1: e.target.value,
                                    })
                                  }
                                  defaultValue={classFresher.emailAdmin1}
                                  id="formemail"
                                  placeholder="Enter your Email..."
                                  type="email"
                                  class="form-control form-control"
                                />
                              </div>
                            </form>
                          </div>
                        </div>
                      </div>
                    </div>
                    <div class="col-sm-6 col-xl-4">
                      <div class="card">
                        <div class="card-body" style={{ padding: "10px 20px" }}>
                          <form class="outer-repeater">
                            <div class="mb-3">
                              <label htmlFor="formname" class="form-label">
                                Name :
                              </label>
                              <input
                                onChange={(e) =>
                                  setClassInfo({
                                    ...classInfo,
                                    nameAdmin2: e.target.value,
                                  })
                                }
                                defaultValue={classFresher.nameAdmin2}
                                id="formname"
                                placeholder="Enter your Name..."
                                type="text"
                                class="form-control form-control"
                              />
                            </div>
                            <div class="mb-3">
                              <label htmlFor="formemail" class="form-label">
                                Email :
                              </label>
                              <input
                                onChange={(e) =>
                                  setClassInfo({
                                    ...classInfo,
                                    emailAdmin2: e.target.value,
                                  })
                                }
                                defaultValue={classFresher.emailAdmin2}
                                id="formemail"
                                placeholder="Enter your Email..."
                                type="email"
                                class="form-control form-control"
                              />
                            </div>
                          </form>
                        </div>
                      </div>
                    </div>
                    <div class="col-sm-6 col-xl-4">
                      <div class="card">
                        <div class="card-body" style={{ padding: "10px 20px" }}>
                          <form class="outer-repeater">
                            <div class="mb-3">
                              <label htmlFor="formname" class="form-label">
                                Name :
                              </label>
                              <input
                                onChange={(e) =>
                                  setClassInfo({
                                    ...classInfo,
                                    nameAdmin3: e.target.value,
                                  })
                                }
                                defaultValue={classFresher.nameAdmin3}
                                id="formname"
                                placeholder="Enter your Name..."
                                type="text"
                                class="form-control form-control"
                              />
                            </div>
                            <div class="mb-3">
                              <label htmlFor="formemail" class="form-label">
                                Email :
                              </label>
                              <input
                                onChange={(e) =>
                                  setClassInfo({
                                    ...classInfo,
                                    emailAdmin3: e.target.value,
                                  })
                                }
                                defaultValue={classFresher.emailAdmin3}
                                id="formemail"
                                placeholder="Enter your Email..."
                                type="email"
                                class="form-control form-control"
                              />
                            </div>
                          </form>
                        </div>
                      </div>
                    </div>
                  </div>
                </div>
                <h4>Trainer</h4>
                <div class="row" style={{ width: "100%" }}>
                  <div class="col-sm-6 col-xl-4">
                    <div class="card">
                      <div class="card-body" style={{ padding: "10px 20px" }}>
                        <form class="outer-repeater">
                          <div class="mb-3">
                            <label htmlFor="formname" class="form-label">
                              Name :
                            </label>
                            <input
                              onChange={(e) =>
                                setClassInfo({
                                  ...classInfo,
                                  nameTrainer1: e.target.value,
                                })
                              }
                              defaultValue={classFresher.nameTrainer1}
                              id="formname"
                              placeholder="Enter your Name..."
                              type="text"
                              class="form-control form-control"
                            />
                          </div>
                          <div class="mb-3">
                            <label htmlFor="formemail" class="form-label">
                              Email :
                            </label>
                            <input
                              onChange={(e) =>
                                setClassInfo({
                                  ...classInfo,
                                  emailTrainer1: e.target.value,
                                })
                              }
                              defaultValue={classFresher.emailTrainer1}
                              id="formemail"
                              placeholder="Enter your Email..."
                              type="email"
                              class="form-control form-control"
                            />
                          </div>
                        </form>
                      </div>
                    </div>
                  </div>
                  <div class="col-sm-6 col-xl-4">
                    <div class="card">
                      <div class="card-body" style={{ padding: "10px 20px" }}>
                        <form class="outer-repeater">
                          <div class="mb-3">
                            <label htmlFor="formname" class="form-label">
                              Name :
                            </label>
                            <input
                              onChange={(e) =>
                                setClassInfo({
                                  ...classInfo,
                                  nameTrainer2: e.target.value,
                                })
                              }
                              defaultValue={classFresher.nameTrainer2}
                              id="formname"
                              placeholder="Enter your Name..."
                              type="text"
                              class="form-control form-control"
                            />
                          </div>
                          <div class="mb-3">
                            <label htmlFor="formemail" class="form-label">
                              Email :
                            </label>
                            <input
                              onChange={(e) =>
                                setClassInfo({
                                  ...classInfo,
                                  emailTrainer2: e.target.value,
                                })
                              }
                              defaultValue={classFresher.emailTrainer2}
                              id="formemail"
                              placeholder="Enter your Email..."
                              type="email"
                              class="form-control form-control"
                            />
                          </div>
                        </form>
                      </div>
                    </div>
                  </div>
                  <div class="col-sm-6 col-xl-4">
                    <div class="card">
                      <div class="card-body" style={{ padding: "10px 20px" }}>
                        <form class="outer-repeater">
                          <div class="mb-3">
                            <label htmlFor="formname" class="form-label">
                              Name :
                            </label>
                            <input
                              onChange={(e) =>
                                setClassInfo({
                                  ...classInfo,
                                  nameTrainer3: e.target.value,
                                })
                              }
                              defaultValue={classFresher.nameTrainer3}
                              id="formname"
                              placeholder="Enter your Name..."
                              type="text"
                              class="form-control form-control"
                            />
                          </div>
                          <div class="mb-3">
                            <label htmlFor="formemail" class="form-label">
                              Email :
                            </label>
                            <input
                              onChange={(e) =>
                                setClassInfo({
                                  ...classInfo,
                                  emailTrainer3: e.target.value,
                                })
                              }
                              defaultValue={classFresher.emailTrainer3}
                              id="formemail"
                              placeholder="Enter your Email..."
                              type="email"
                              class="form-control form-control"
                            />
                          </div>
                        </form>
                      </div>
                    </div>
                  </div>
                </div>
              </div>
              <div style={{ display: "flex", justifyContent: "end" }}>
                <button
                  onClick={ConfirmClassFresher}
                  type="button"
                  style={{ marginRight: "12px" }}
                  class="btn btn-success"
                >
                  <SaveIcon /> &nbsp; Save
                </button>
              </div>
              <h4>List freshers</h4>
              <DataTableClassFresher
                freshers={classFresher.freshers}
                isEdit={false}
              ></DataTableClassFresher>
            </div>
          ) : null}
        </div>
      </div>
    </div>
  );
}
