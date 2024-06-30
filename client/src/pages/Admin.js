import React, {useState} from 'react';
import {Button} from "react-bootstrap";
import AdminUserGrid from "../components/modals/Admin/AdminUserGrid";

const Admin = () => {
    const [visUser, setVisUser] = useState(false)

    return (
        <div>
        <div className="d-flex flex-column MContent Block">

            <Button
                variant={"outline-dark"}
                className="mt-4 p-2"
                onClick={() => setVisUser(true)}
            >
                Редактировать пользователей
            </Button>

        </div>
            <AdminUserGrid show={visUser} onHide={() => setVisUser(false)}/>
        </div>
    );
};

export default Admin;