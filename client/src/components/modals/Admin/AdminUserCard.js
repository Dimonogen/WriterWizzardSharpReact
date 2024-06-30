import React, {useState} from 'react';
import Modal from "react-bootstrap/Modal";
import {Button, Form} from "react-bootstrap";
import {DateFormat} from "../../../utils/functions";
import YesOfCouseButton from "../../YesOfCouseButton";

const AdminUserCard = ({show, onHide, userData}) => {
    let classModal = 'modal-my ';



    return (
        <div onClick={(e)=>{if(e.target.id == 'ModalAdminUserCard'){onHide()}}}
             id="ModalAdminUserCard" className={classModal + (show ? 'd-block' : 'd-none')}>
            <div className="modal-content-my W-50">
                <div className="modal-header-my ">
                    <h4 className='text-dark'>Id = {userData?.id}</h4>
                </div>
                <div className='modal-body-my d-flex flex-column'>
                    <span>name: {userData?.name}</span>
                    <span>email: {userData?.email}</span>
                    <span>data reg: {DateFormat(new Date(userData?.createdAt))}</span>
                    <spam>Действия:</spam>
                    <div className='mt-3 mb-2 d-flex flex-row'>
                        <YesOfCouseButton variant='outline-danger' final={() => {alert('не реализовано')}} classNames='me-3'
                                          title={'Вы действительно хотите изменить пароль пользователя "'+userData?.name+'" ?'} props={'Изменить пароль'}/>
                        <YesOfCouseButton variant='outline-danger' final={() => {alert('не реализовано')}}
                                          title={'Вы действительно хотите удалить пользователя "'+userData?.name+'" ?'} props={'Удалить'}/>
                    </div>
                </div>
                <div className="modal-footer-my">
                    <Button className='me-2' variant='outline-danger' onClick={() => onHide()}>Закрыть</Button>
                </div>
            </div>
        </div>

    );
};

export default AdminUserCard;