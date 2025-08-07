import React, {useState} from 'react';
import {Button} from "react-bootstrap";

const ModalYesNo = ({show, onHide, final, title, yestitle, notitle}) => {

    let classModal = 'modal-my ';

    return (
        <div onClick={(e)=>{if(e.target.id == 'myModal'){onHide()}}}
             id="myModal" className={classModal + (show ? 'd-block' : 'd-none')}>
            <div className="modal-content-my W-50">
                <div className="modal-header-my ">
                    <h4 className='text-dark'>{title}</h4>
                </div>
                <div className="modal-footer-my">

                    <Button className='me-2' variant='outline-success' onClick={() => onHide()}>{notitle}</Button>
                    <Button variant='outline-danger' onClick={() => {final(); onHide()}}>{yestitle}</Button>
                </div>
            </div>
        </div>

    );
};

export default ModalYesNo;