import React, { useState } from 'react';
import {
    MenuFoldOutlined,
    MenuUnfoldOutlined,
    UserOutlined,
    CompassFilled,
    BankFilled,
    HomeFilled,
    CalendarFilled
} from '@ant-design/icons';
import { Button, Layout, Menu, theme } from 'antd';
import { Outlet, useLocation, useNavigate, useParams } from 'react-router-dom';

const { Header, Sider, Content } = Layout;

const App: React.FC = () => {
    const navigate = useNavigate();
    const location = useLocation();
    const [collapsed, setCollapsed] = useState(false);
    const {
        token: { colorBgContainer, borderRadiusLG },
    } = theme.useToken();

    return (
        <Layout style={{ height: "100vh", overflow: "auto" }}>
            <Sider trigger={null} collapsible collapsed={collapsed}>
                <div className="demo-logo-vertical" />
                <Menu
                    theme="dark"
                    mode="inline"
                    onClick={(event) => {
                        navigate(event.key)
                    }}
                    defaultSelectedKeys={['/']}
                    selectedKeys={[location.pathname]}
                    items={[
                        {
                            key: '/',
                            icon: <HomeFilled />,
                            label: 'Home'
                        },
                        {
                            key: '/countries',
                            icon: <CompassFilled />,
                            label: 'Countries',
                        },
                        {
                            key: '/departments',
                            icon: <BankFilled />,
                            label: 'Departments',
                        },
                        {
                            key: '/employees',
                            icon: <UserOutlined />,
                            label: 'Employees',
                        },
                        {
                            key: '/jobs',
                            icon: <CalendarFilled />,
                            label: 'Jobs',
                        },
                        {
                            key: '/jobHistories',
                            icon: <CalendarFilled />,
                            label: 'Job Histories',
                        },
                        {
                            key: '/locations',
                            icon: <CalendarFilled />,
                            label: 'Locations',
                        },
                        {
                            key: '/regions',
                            icon: <CalendarFilled />,
                            label: 'Regionss',
                        },
                    ]}
                />
            </Sider>
            <Layout>
                <Header style={{ padding: 0, background: colorBgContainer }}>
                    <Button
                        type="text"
                        icon={collapsed ? <MenuUnfoldOutlined /> : <MenuFoldOutlined />}
                        onClick={() => setCollapsed(!collapsed)}
                        style={{
                            fontSize: '16px',
                            width: 64,
                            height: 64,
                        }}
                    />
                </Header>
                <Content style={{ margin: '24px 16px 0', overflow: 'initial', width: '100%', height: '100%' }}>
                    <div
                        style={{
                            padding: 24,
                            textAlign: 'center',
                            background: colorBgContainer,
                            borderRadius: borderRadiusLG,
                        }}
                    >
                        <Outlet />

                    </div>
                </Content>
            </Layout>
        </Layout>
    );
};

export default App;