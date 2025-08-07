import React, {useState} from 'react';
import {Button} from "react-bootstrap";

const ModalYesNo = ({show, onHide, title, okTitle}) => {

    let classModal = 'modal-my ';

    return (
        <div onClick={(e)=>{if(e.target.id == 'myModalOk'){onHide()}}}
             id="myModalOk" className={classModal + (show ? 'd-block' : 'd-none')}>
            <div className="modal-content-my W-50">
                <div className="modal-header-my ">
                    <h4 className='text-dark'>{title}</h4>
                </div>
                <div className="modal-footer-my">
                    <Button className='me-2' variant='outline-dark' onClick={() => onHide()}>{okTitle}</Button>
                    </div>
            </div>
        </div>

    );
};

export default ModalYesNo;