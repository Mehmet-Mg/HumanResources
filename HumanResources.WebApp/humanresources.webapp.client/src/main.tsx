import { StrictMode } from 'react'
import { createRoot } from 'react-dom/client'
import { createBrowserRouter, RouterProvider } from 'react-router-dom';

import Root from './routes/root.tsx';
import ErrorPage from './error-page.tsx';

import Country from './routes/Country.tsx'
import Department from './routes/Department.tsx';
import Employee from './routes/Employee.tsx';
import Job from './routes/Job.tsx';
import JobHistory from './routes/JobHistory.tsx';
import Location from './routes/Location.tsx';
import Region from './routes/Region.tsx';

import './index.css';

const router = createBrowserRouter([
    {
        path: "/",
        element: <Root />,
        errorElement: <ErrorPage />,
        children: [
            {
                path: "/countries",
                element: <Country />
            },
            {
                path: "/departments",
                element: <Department />
            },
            {
                path: "/employees",
                element: <Employee />
            },
            {
                path: "/jobs",
                element: <Job />
            },
            {
                path: "/jobHistories",
                element: <JobHistory />
            },
            {
                path: "/locations",
                element: <Location />
            },
            {
                path: "/regions",
                element: <Region />
            }
        ]
    },
]);

createRoot(document.getElementById('root')!).render(
    <StrictMode>
        <RouterProvider router={router} />
    </StrictMode>,
)
