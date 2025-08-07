import React, {useEffect, useState} from 'react';
import {Button, FormCheck, Table} from "react-bootstrap";
import {AdmGetAllUsers} from "../../../http/AdminAPI";
import AdminUserCard from "./AdminUserCard";

const AdminUserGrid = ({show, onHide}) => {

    let classModal = 'modal-my ';

    useEffect( () => {
        AdmGetAllUsers().then(data => {setUsers(data); });
    }, [])

    const [users, setUsers] = useState([])
    const [viewCard, setViewCard] = useState(false)
    const [selUserData, setselUserData] = useState(null)

    return (
        <div onClick={(e)=>{if(e.target.id === 'ModalAdminUserGrid'){onHide()}}}
             id="ModalAdminUserGrid" className={classModal + (show ? 'd-block' : 'd-none')}>
            <div className="modal-content-my W-90">
                <div className="modal-header-my ">
                    <h4 className='text-dark'>Редактирование пользователей</h4>
                </div>
                <div className='modal-body-my'>
                    Грид всех пользователей
                    <Table className='mt-3' striped hover>
                        <thead>
                        <tr>
                            <th>#</th>
                            <th>Id</th>
                            <th>Name</th>
                            <th>Login</th>
                            <th></th>
                        </tr>
                        </thead>
                        <tbody>
                        {
                            users.length > 0
                                ?
                                users.map(elem =>
                                    <tr key={elem.id}>
                                        <td><FormCheck/></td>
                                        <td>{elem.id}</td>
                                        <td>{elem.name}</td>
                                        <td>{elem.email}</td>
                                        <td><Button variant='outline-secondary' onClick={() =>
                                        {setViewCard(true); setselUserData(elem)} }>View</Button></td>
                                    </tr>
                                )
                                :
                                null
                        }
                        </tbody>
                    </Table>
                </div>
                <div className="modal-footer-my">

                    <Button className='me-2' variant="outline-danger" onClick={onHide}>Закрыть</Button>
                </div>
            </div>
            <AdminUserCard show={viewCard} onHide={() => setViewCard(false)} userData={selUserData} />
        </div>

    );
};

export default AdminUserGrid;