import * as React from 'react';
import Box from '@mui/material/Box';
import { Container } from '@mui/system';
import { DataGrid, GridToolbarContainer, GridToolbarColumnsButton, GridToolbarFilterButton, gridPageCountSelector, gridPageSelector, useGridApiContext, useGridSelector, } from '@mui/x-data-grid';
import Button from '@mui/material/Button';
import { Stack } from '@mui/material';
import FileDownloadIcon from '@mui/icons-material/FileDownload';
import ErrorIcon from '@mui/icons-material/Error';
import InfoIcon from '@mui/icons-material/Info';
import axios from "axios";
import { FresherReportCompletionStatus, FresherReportStatus } from '../../configs/enums';
import PropTypes from 'prop-types';
import Tabs from '@mui/material/Tabs';
import Tab from '@mui/material/Tab';
import Typography from '@mui/material/Typography';
import Backdrop from '@mui/material/Backdrop';
import CircularProgress from '@mui/material/CircularProgress';
import MuiAlert from '@mui/material/Alert';
import Pagination from '@mui/material/Pagination';
import { configEnv } from '../../configs/config'
import config from '../../config/config'

const columns = [
    { field: 'account', headerName: 'Account', width: 150 },
    { field: 'name', headerName: 'Name', width: 200 },
    { field: 'educationInfo', headerName: 'Education Info', width: 150 },
    { field: 'universityId', headerName: 'University Id', width: 200 },
    { field: 'University Name', headerName: 'University Name', width: 200 },
    { field: 'major', headerName: 'Major', width: 200 },
    { field: 'universityGraduationDate', headerName: 'University Graduation Date', width: 200 },
    { field: 'universityGPA', headerName: 'University GPA', width: 200 },
    { field: 'educationLevel', headerName: 'Education Level', width: 200 },
    { field: 'branch', headerName: 'Branch', width: 200 },
    { field: 'parentDepartment', headerName: 'Parent Department', width: 200 },
    { field: 'site', headerName: 'Site', width: 150 },
    { field: 'courseCode', headerName: 'Course Code', width: 250 },
    { field: 'courseName', headerName: 'Course Name', width: 250 },
    { field: 'subjectType', headerName: 'Subject Type', width: 200 },
    { field: 'subSubjectType', headerName: 'Sub-Subject Type', width: 200 },
    { field: 'formatType', headerName: 'Format Type', width: 200 },
    { field: 'scope', headerName: 'Scope', width: 200 },
    { field: 'fromDate', headerName: 'FromDate', width: 200 },
    { field: 'toDate', headerName: 'To Date', width: 200 },
    { field: 'learningTime', headerName: 'Learning Time', width: 200 },
    { field: 'status', headerName: 'Status', width: 200 },
    { field: 'finalGrade', headerName: 'Final Grade', width: 200 },
    { field: 'completionLevel', headerName: 'Completion Level', width: 200 },
    { field: 'statusAllocated', headerName: 'Status Allocated', width: 200 },
    { field: 'salaryAllocated', headerName: 'Salary Allocated', width: 200 },
    { field: 'fsuAllocated', headerName: 'FSU Allocated', width: 200 },
    { field: 'buAllocated', headerName: 'BU Allocated', width: 200 },
    { field: 'toeicGrade', headerName: 'TOEIC Grade', width: 200 },
    { field: 'languageSkill', headerName: 'Language Skill', width: 200 },
    { field: 'updatedBy', headerName: 'Updated By', width: 200 },
    { field: 'updatedDate', headerName: 'Updated Date', width: 200 },
    { field: 'note', headerName: 'Note', width: 400 },
    { field: 'employeeValid', headerName: 'Employee Valid', width: 200 },
    { field: 'courseStatus', headerName: 'Course Status', width: 200 },
    { field: 'courseValidOrSubjectType', headerName: 'Course Valid/Subject Type', width: 200 },
    { field: 'courseValidOrSubsubjectType', headerName: 'Course Valid/Subsubject Type', width: 200 },
    { field: 'courseValidOrFormatType', headerName: 'Course Valid/Format Type', width: 200 },
    { field: 'courseValidOrScope', headerName: 'Course Valid/Scope', width: 200 },
    { field: 'courseValidOrStartDate', headerName: 'Course Valid/Start Date', width: 200 },
    { field: 'courseValidOrEndDate', headerName: 'Course Valid/End Date', width: 200 },
    { field: 'courseValidOrLearningTime', headerName: 'Course Valid/Learning Time', width: 200 },
    { field: 'endYear', headerName: 'End Year', width: 200 },
    { field: 'endMonth', headerName: 'End Month', width: 200 },
];

const classCode = ['HCM22_FR_NET_01', 'HCM22_FR_NET_02', 'HCM22_FR_NET_03', 'HCM22_FR_NET_04', 'HCM22_FR_NET_05'];

function TabPanel(props) {
    const { children, value, index, ...other } = props;

    return (
        <div
            role="tabpanel"
            hidden={value !== index}
            id={`simple-tabpanel-${index}`}
            aria-labelledby={`simple-tab-${index}`}
            {...other}
        >
            {value === index && (
                <Box sx={{ p: 3 }}>
                    <Typography>{children}</Typography>
                </Box>
            )}
        </div>
    );
}

TabPanel.propTypes = {
    children: PropTypes.node,
    index: PropTypes.number.isRequired,
    value: PropTypes.number.isRequired,
};

function a11yProps(index) {
    return {
        id: `simple-tab-${index}`,
        'aria-controls': `simple-tabpanel-${index}`,
    };
}

function CustomGridToolbar() {
    return (
        <GridToolbarContainer>
            <GridToolbarColumnsButton />
            <GridToolbarFilterButton />
        </GridToolbarContainer>
    );
}

function CustomPagination() {
    const apiRef = useGridApiContext();
    const page = useGridSelector(apiRef, gridPageSelector);
    const pageCount = useGridSelector(apiRef, gridPageCountSelector);

    return (
        <Pagination
            color="primary"
            count={pageCount}
            page={page + 1}
            onChange={(event, value) => apiRef.current.setPage(value - 1)}
        />
    );
}

const customDataGridStyles = {
    width: '100%',
    '& .MuiDataGrid-main': {
        backgroundColor: 'white',
    },
    '& .MuiDataGrid-toolbarContainer': {
        backgroundColor: '#f7f7f7',
    },
    '& .MuiDataGrid-columnHeaders': {
        backgroundColor: '#f7f7f7',
    },
    '& .MuiDataGrid-footerContainer': {
        backgroundColor: '#f7f7f7',
    },
    '& .MuiDataGrid-columnHeader, .MuiDataGrid-cell': {
        borderRight: '1px solid #f0f0f0',
    },
    '& .MuiDataGrid-columnHeaderTitle': {
        fontWeight: 'bold',
    },
}

const Alert = React.forwardRef(function Alert(props, ref) {
    return <MuiAlert elevation={6} ref={ref} variant="filled" {...props} />;
});

export default function FresherReportView() {
    const [weeklyReportData, setWeeklyReportData] = React.useState('');
    const [monthlyReportData, setMonthlyReportData] = React.useState('');
    const [weeklyReportDataGrid, setWeeklyReportDataGrid] = React.useState([]);
    const [monthlyReportDataGrid, setMonthlyReportDataGrid] = React.useState([]);
    const [openDialog, setOpenDialog] = React.useState(false);
    const [courseCodeInput, setCourseCode] = React.useState(classCode[0]);
    const [reportType, setReportType] = React.useState('weeklyReport');
    const [tabValue, setTabValue] = React.useState(0);
    const [backDrop, setBackDrop] = React.useState(false);
    const [token, setToken] = React.useState(localStorage.getItem(config.tokenKey));
    const [errorMessage, setErrorMessage] = React.useState('');

    const generateReportData = async (courseCode, isMonthly) => {
        await axios.get(configEnv.FETCH_STRING + 'FresherReport/GenerateFresherReport?isMonthly=' + isMonthly, {
            headers: {
                'Authorization': 'Bearer ' + token,
            }
        }).then((response) => {
            if (response.data !== '') {
                if (isMonthly === 'false') {
                    setWeeklyReportData(response.data);
                    const reportDataGrid = JSON.parse(JSON.stringify(response.data));
                    reportDataGrid.forEach(element => {
                        element.status = FresherReportStatus[element.status];
                        element.completionLevel = FresherReportCompletionStatus[element.completionLevel];
                        element.courseStatus = FresherReportStatus[element.courseStatus];
                    });
                    setWeeklyReportDataGrid(reportDataGrid);
                }
                else {
                    setMonthlyReportData(response.data);
                    const reportDataGrid = JSON.parse(JSON.stringify(response.data));
                    reportDataGrid.forEach(element => {
                        element.status = FresherReportStatus[element.status];
                        element.completionLevel = FresherReportCompletionStatus[element.completionLevel];
                        element.courseStatus = FresherReportStatus[element.courseStatus];
                    });
                    setMonthlyReportDataGrid(reportDataGrid);
                }
            }
        }).catch((error) => {
            console.log(error);
            handleErrorMessage(error.response.status);
        });
    }

    React.useEffect(() => {
        (
            async () => {
                await generateReportData(courseCodeInput, 'false');
            }
        )();
    }, [])

    const handleErrorMessage = (errorStatusCode) => {
        switch (errorStatusCode) {
            case 0:
                setErrorMessage('Server downed.');
                break;
            case 401:
                setErrorMessage('You are not login.');
                break;
            case 403:
                setErrorMessage('You are not login.');
                break;
            default:
                setErrorMessage('An error has occurred.');
                break;
        }
    };

    const handleChange = (event, newValue) => {
        setTabValue(newValue);
    };

    const handleCourseCodeChanged = (event, newValue) => {
        setCourseCode(newValue);
    };

    const handleClickOpen = (event) => {
        setReportType(event.currentTarget.id);
        setOpenDialog(true);
    };

    const handleSubmit = async (courseCode) => {
        if (reportType === 'weeklyReport') {
            setWeeklyReportData('');
            setWeeklyReportDataGrid([]);
            setMonthlyReportData('');
            setMonthlyReportDataGrid([]);
            await generateReportData(courseCode, 'false');
        }
        if (reportType === 'monthlyReport') {
            setWeeklyReportData('');
            setWeeklyReportDataGrid([]);
            setMonthlyReportData('');
            setMonthlyReportDataGrid([]);
            await generateReportData(courseCode, 'true');
        }
    };

    const handleClose = () => {
        setOpenDialog(false);
    };

    const handleTabClick = async (reportType) => {
        setReportType(reportType);
        if (reportType === 'weeklyReport') {
            setWeeklyReportData('');
            setWeeklyReportDataGrid([]);
            setErrorMessage('');
            await generateReportData(courseCodeInput, 'false');
        }
        else {
            setMonthlyReportData('');
            setMonthlyReportDataGrid([]);
            setErrorMessage('');
            await generateReportData(courseCodeInput, 'true');
        }
    }

    const handleClickExport = async (event) => {
        setBackDrop(false);
        switch (event.currentTarget.id) {
            case 'exportWeeklyReportButton':
                if (weeklyReportData !== '') {
                    setBackDrop(true);
                    axios.post(configEnv.FETCH_STRING + 'export/employee-training-history', weeklyReportData, {
                        headers: {
                            'Content-Type': 'application/json'
                        },
                        responseType: 'blob'
                    }).then(response => {
                        const url = window.URL.createObjectURL(new Blob([response.data]));
                        const link = document.createElement('a');
                        link.href = url;
                        link.setAttribute('download', response.headers['file-name']);
                        document.body.appendChild(link);
                        link.click();
                        setBackDrop(false);
                    });
                }
                break;
            case 'exportMonthlyReportButton':
                if (monthlyReportData !== '') {
                    setBackDrop(true);
                    axios.post(configEnv.FETCH_STRING + 'export/employee-training-history', monthlyReportData, {
                        headers: {
                            'Content-Type': 'application/json'
                        },
                        responseType: 'blob'
                    }).then(response => {
                        const url = window.URL.createObjectURL(new Blob([response.data]));
                        const link = document.createElement('a');
                        link.href = url;
                        link.setAttribute('download', response.headers['file-name']);
                        document.body.appendChild(link);
                        link.click();
                        setBackDrop(false);
                    });
                }
                break;
            default:
                break;
        }
    };

    if (!weeklyReportData || !monthlyReportData) return (
        <Box sx={{ width: '100%' }} style={{ marginTop: 30 }}>
            <Container maxWidth='xl'>
                <Box sx={{ borderBottom: 1, borderColor: 'divider' }}>
                    <Tabs value={tabValue} onChange={handleChange} aria-label="basic tabs example">
                        <Tab label="Weekly Report" {...a11yProps(0)} onClick={e => handleTabClick('weeklyReport')} />
                        <Tab label="Monthly Report" {...a11yProps(1)} onClick={e => handleTabClick('monthlyReport')} />
                    </Tabs>
                </Box>
                <TabPanel value={tabValue} index={0}>
                    <Stack spacing={1} direction="row" style={{ marginBottom: 10 }}>
                        <Button id="exportWeeklyReportButton" color="success" variant="contained" startIcon={<FileDownloadIcon />} onClick={handleClickExport}>Export To Excel</Button>
                        <Backdrop
                            sx={{ color: '#fff', zIndex: (theme) => theme.zIndex.drawer + 1 }}
                            open={backDrop}
                            onClick={handleClose}
                        >
                            <CircularProgress color="inherit" />
                        </Backdrop>
                    </Stack>
                    <div style={{ height: 600, width: '100%' }}>
                        <DataGrid
                            sx={customDataGridStyles}
                            rows={weeklyReportDataGrid}
                            columns={columns}
                            pageSize={8}
                            rowsPerPageOptions={[8]}
                            disableSelectionOnClick
                            pagination
                            components={{
                                Toolbar: CustomGridToolbar,
                                Pagination: CustomPagination,
                                NoRowsOverlay: () => (
                                    <Stack spacing={1} height="100%" direction="column" alignItems="center" justifyContent="center">
                                        {errorMessage === '' ? <CircularProgress /> : <ErrorIcon color='error' fontSize='large' />}
                                        {errorMessage === '' ? <p>Getting fresher data...</p> : <p>{errorMessage}</p>}
                                    </Stack>
                                ),
                                NoResultsOverlay: () => (
                                    <Stack spacing={1} height="100%" direction="column" alignItems="center" justifyContent="center">
                                        <InfoIcon color='warning' fontSize='large' />
                                        <p>No result</p>
                                    </Stack>
                                )
                            }}
                        />
                    </div>
                </TabPanel>
                <TabPanel value={tabValue} index={1}>
                    <Stack spacing={1} direction="row" style={{ marginBottom: 10 }}>
                        <Button id="exportMonthlyReportButton" color="success" variant="contained" startIcon={<FileDownloadIcon />} onClick={handleClickExport}>Export To Excel</Button>
                        <Backdrop
                            sx={{ color: '#fff', zIndex: (theme) => theme.zIndex.drawer + 1 }}
                            open={backDrop}
                            onClick={handleClose}
                        >
                            <CircularProgress color="inherit" />
                        </Backdrop>
                    </Stack>
                    <div style={{ height: 600, width: '100%' }}>
                        <DataGrid
                            sx={customDataGridStyles}
                            rows={monthlyReportDataGrid}
                            columns={columns}
                            pageSize={8}
                            rowsPerPageOptions={[8]}
                            disableSelectionOnClick
                            pagination
                            components={{
                                Toolbar: CustomGridToolbar,
                                Pagination: CustomPagination,
                                NoRowsOverlay: () => (
                                    <Stack spacing={1} height="100%" direction="column" alignItems="center" justifyContent="center">
                                        {errorMessage === '' ? <CircularProgress /> : <ErrorIcon color='error' fontSize='large' />}
                                        {errorMessage === '' ? <p>Getting fresher data...</p> : <p>{errorMessage}</p>}
                                    </Stack>
                                ),
                                NoResultsOverlay: () => (
                                    <Stack spacing={1} height="100%" direction="column" alignItems="center" justifyContent="center">
                                        <InfoIcon color='warning' fontSize='large' />
                                        <p>No result</p>
                                    </Stack>
                                )
                            }}
                        />
                    </div>
                </TabPanel>
            </Container>
        </Box>
    );

    return (
        <Box sx={{ width: '100%' }} style={{ marginTop: 30 }}>
            <Container maxWidth='xl'>
                <Box sx={{ borderBottom: 1, borderColor: 'divider' }}>
                    <Tabs value={tabValue} onChange={handleChange} aria-label="basic tabs example">
                        <Tab label="Weekly Report" {...a11yProps(0)} onClick={e => setReportType('weeklyReport')} />
                        <Tab label="Monthly Report" {...a11yProps(1)} onClick={e => setReportType('monthlyReport')} />
                    </Tabs>
                </Box>
                <TabPanel value={tabValue} index={0}>
                    <Stack spacing={1} direction="row" style={{ marginBottom: 10 }}>
                        <Button id="exportWeeklyReportButton" color="success" variant="contained" startIcon={<FileDownloadIcon />} onClick={handleClickExport}>Export To Excel</Button>
                        <Backdrop
                            sx={{ color: '#fff', zIndex: (theme) => theme.zIndex.drawer + 1 }}
                            open={backDrop}
                            onClick={handleClose}
                        >
                            <CircularProgress color="inherit" />
                        </Backdrop>
                    </Stack>
                    <div style={{ height: 600, width: '100%' }}>
                        <DataGrid
                            sx={customDataGridStyles}
                            rows={weeklyReportDataGrid}
                            columns={columns}
                            pageSize={8}
                            rowsPerPageOptions={[8]}
                            disableSelectionOnClick
                            pagination
                            components={{
                                Toolbar: CustomGridToolbar,
                                Pagination: CustomPagination,
                                NoRowsOverlay: () => (
                                    <Stack spacing={1} height="100%" direction="column" alignItems="center" justifyContent="center">
                                        {errorMessage === '' ? <CircularProgress /> : <ErrorIcon color='error' fontSize='large' />}
                                        {errorMessage === '' ? <p>Getting fresher data...</p> : <p>{errorMessage}</p>}
                                    </Stack>
                                ),
                                NoResultsOverlay: () => (
                                    <Stack spacing={1} height="100%" direction="column" alignItems="center" justifyContent="center">
                                        <InfoIcon color='warning' fontSize='large' />
                                        <p>No result</p>
                                    </Stack>
                                )
                            }}
                        />
                    </div>
                </TabPanel>
                <TabPanel value={tabValue} index={1}>
                    <Stack spacing={1} direction="row" style={{ marginBottom: 10 }}>
                        <Button id="exportMonthlyReportButton" color="success" variant="contained" startIcon={<FileDownloadIcon />} onClick={handleClickExport}>Export To Excel</Button>
                        <Backdrop
                            sx={{ color: '#fff', zIndex: (theme) => theme.zIndex.drawer + 1 }}
                            open={backDrop}
                            onClick={handleClose}
                        >
                            <CircularProgress color="inherit" />
                        </Backdrop>
                    </Stack>
                    <div style={{ height: 600, width: '100%' }}>
                        <DataGrid
                            sx={customDataGridStyles}
                            rows={monthlyReportDataGrid}
                            columns={columns}
                            pageSize={8}
                            rowsPerPageOptions={[8]}
                            disableSelectionOnClick
                            pagination
                            components={{
                                Toolbar: CustomGridToolbar,
                                Pagination: CustomPagination,
                                NoRowsOverlay: () => (
                                    <Stack spacing={1} height="100%" direction="column" alignItems="center" justifyContent="center">
                                        {errorMessage === '' ? <CircularProgress /> : <ErrorIcon color='error' fontSize='large' />}
                                        {errorMessage === '' ? <p>Getting fresher data...</p> : <p>{errorMessage}</p>}
                                    </Stack>
                                ),
                                NoResultsOverlay: () => (
                                    <Stack spacing={1} height="100%" direction="column" alignItems="center" justifyContent="center">
                                        <InfoIcon color='warning' fontSize='large' />
                                        <p>No result</p>
                                    </Stack>
                                )
                            }}
                        />
                    </div>
                </TabPanel>
            </Container>
        </Box>
    );
}
