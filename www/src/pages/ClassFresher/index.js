import React, { useState, useEffect } from "react";
import axios from "axios";
import DataTableClassFresher from "../../components/ClassFresher/DataTableClassFresher";
import { configEnv } from '../../configs/config';
import { useHistory } from "react-router-dom";
import ControlPointIcon from "@mui/icons-material/ControlPoint";
import CloudUploadIcon from "@mui/icons-material/CloudUpload";

export default function ClassFresherPage() {
  const [listClass, setListClass] = useState();
  const [page, setPage] = useState(0);

  useEffect(() => {
    
    let isActive = false;
    if (!isActive) {
      axios
        .get(
          `${configEnv.FETCH_STRING}ClassFresher/GetAllClassFresherPagingsion?pageIndex=${page}&pageSize=10`
        )
        .then((res) => {
          setListClass(res.data.items);
        });
    }
    return () => {
      isActive = true;
    };
  }, [page]);

  const GotoNextPage = () => {
    if (page !== 0) {
      let next = page + 1;
      setPage(next);
    }
  };

  const history = useHistory();
  const GotoImport = (e) => {
    history.push(`classes/import`);
  };
  const GotoCreate = (e) => {
    history.push(`classes/create`);
  };

  return (
    <div className="class-fresher">      
      <div className="group-btn-classfresher">
        <button onClick={GotoImport} type="button" class="btn btn-primary w-sm">
          <CloudUploadIcon></CloudUploadIcon>&nbsp; Import Class
        </button>
        &nbsp;&nbsp;&nbsp;
        {/* <button onClick={GotoCreate} type="button" class="btn btn-success w-sm">
          <ControlPointIcon></ControlPointIcon>&nbsp; Create Class
        </button> */}
      </div>
      {listClass !== undefined ? (
        <DataTableClassFresher classFresher={listClass}></DataTableClassFresher>
      ) : (
        <div>No class</div>
      )}
    </div>
  );
}
