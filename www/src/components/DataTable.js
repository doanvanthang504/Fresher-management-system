<<<<<<< HEAD
import React, { useState } from 'react';

const empList = [
  { id: 1, name: "Neeraj", email: 'neeraj@gmail.com', status: 0, role: 1 },
  { id: 2, name: "Raj", email: 'raj@gmail.com', status: 1, role: 0 },
  { id: 3, name: "David", email: 'david342@gmail.com', status: 1, role: 3 },
  { id: 4, name: "Vikas", email: 'vikas75@gmail.com', status: 0, role: 2 },
  { id: 1, name: "Neeraj", email: 'neeraj@gmail.com', status: 0, role: 1 },
  { id: 2, name: "Raj", email: 'raj@gmail.com', status: 1, role: 0 },
  { id: 3, name: "David", email: 'david342@gmail.com', status: 1, role: 3 },
  { id: 4, name: "Vikas", email: 'vikas75@gmail.com', status: 0, role: 2 },
  { id: 1, name: "Neeraj", email: 'neeraj@gmail.com', status: 0, role: 1 },
  { id: 2, name: "Raj", email: 'raj@gmail.com', status: 1, role: 0 },
  { id: 3, name: "David", email: 'david342@gmail.com', status: 1, role: 3 },
  { id: 4, name: "Vikas", email: 'vikas75@gmail.com', status: 0, role: 2 },
]
=======
import * as React from 'react';
import { DataGrid } from '@mui/x-data-grid';
const axios = require('axios').default;

const columns = [
  { field: '_Id', headerName: '_Id', hide: true },
  { field: 'chemicalType', headerName: 'chemical Type', width: 200 },
  { field: 'preHarvestIntervalInDays', headerName: 'preHarvest Interval InDays', type: 'number', width: 200 },
  {
    field: 'activeIngredient',
    headerName: 'active Ingredient',
    width: 200,
  },
  {
    field: 'name',
    headerName: 'name header',
    //description: 'This column has a value getter and is not sortable.',
    //sortable: false,
    width: 200,
    // valueGetter: (params: GridValueGetterParams) =>
    //   `${params.row.firstName || ''} ${params.row.lastName || ''}`,
  },
  {
    field: 'creationDate',
    headerName: 'creation Date',
    width: 300,
  }
];
>>>>>>> master

function DataTable() {

  const [data, setData] = useState(empList)
  const columns = [
    { title: "ID", field: "id" },
    { title: "Name", field: "name" },
    { title: "Email", field: "email" },
    { title: "Status", field: 'status', },
    { title: "Role", field: "role", }
  ]


  return (
    <div>
      <h1 align="center">React-App</h1>
      <h4 align='center'>Material Table</h4>
    </div>
  );
}

export default DataTable;
