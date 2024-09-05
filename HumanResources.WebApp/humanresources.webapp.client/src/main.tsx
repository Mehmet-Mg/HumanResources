import { StrictMode } from 'react'
import { createRoot } from 'react-dom/client'
import { createBrowserRouter, RouterProvider } from 'react-router-dom';

import Root from './routes/root.tsx';
import ErrorPage from './error-page.tsx';

import Country from './routes/Country.tsx'
import Department from './routes/Department.tsx';
import Employee from './routes/Employee.tsx';

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
                element: <Employee />
            },
            {
                path: "/jobHistories",
                element: <Employee />
            },
            {
                path: "/locations",
                element: <Employee />
            },
            {
                path: "/regions",
                element: <Employee />
            }
        ]
    },
]);

createRoot(document.getElementById('root')!).render(
    <StrictMode>
        <RouterProvider router={router} />
    </StrictMode>,
)
