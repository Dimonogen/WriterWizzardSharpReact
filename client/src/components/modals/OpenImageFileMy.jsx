import React, {useState} from 'react';
import Modal from "react-bootstrap/Modal";
import {Button, Form} from "react-bootstrap";

const OpenImageFile = ({show, onHide, title}) => {
    const [name_v, setName_v] = useState('')
    const [dsc_v, setDsc_v] = useState('')

    let image;

    const selectFile = e => {
        image = e.target.files[0]
    }

    const final = () => {
        onHide(image)
    }

    let classModal = 'modal-my ';

    return (
        <div onClick={(e)=>{if(e.target.id == 'myModal'){onHide()};//onHide()
             }} id="myModal" className={classModal + (show ? 'd-block' : 'd-none')}>
            <div className="modal-content-my W-50">
                <div className="modal-header-my ">
                    <h4 className='text-dark'>{title}</h4>
                </div>
                <div className='modal-body-my'>
                    <Form.Control
                        className="mt-3"
                        type="file"
                        onChange={selectFile}
                    />
                </div>
                <div className="modal-footer-my">
                    <Button className='me-2' variant="outline-danger" onClick={onHide}>Закрыть</Button>
                    <Button variant="outline-success" onClick={final}>Добавить</Button>
                </div>
            </div>
        </div>

    );
};

export default OpenImageFile;
