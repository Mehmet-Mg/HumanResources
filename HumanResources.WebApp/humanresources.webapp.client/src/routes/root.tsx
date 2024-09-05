import { Link, Outlet } from "react-router-dom";

export default function Root() {
    return (
        <>
            <div id="sidebar">
                <h1>Human Resources</h1>
                <div>
                    <form id="search-form" role="search">
                        <input
                            id="q"
                            aria-label="Searh contacts"
                            placeholder="Search"
                            type="search"
                            name="q"
                        />
                        <div
                            id="search-spinner"
                            aria-hidden
                            hidden={true}
                        />
                        <div
                            className="sr-only"
                            aria-live="polite"
                        />
                    </form>
                </div>
                <nav>
                    <ul>
                        <li>
                            <Link to={'/countries'}>Countries</Link>
                        </li>
                        <li>
                            <Link to={'/departments'}>Departments</Link>
                        </li>
                        <li>
                            <Link to={'/employees'}>Employees</Link>
                        </li>
                    </ul>
                </nav>
            </div>
            <div id="detail">
                <Outlet />
            </div>
        </>
    )
}