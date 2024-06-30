import React, {useState} from 'react';
import Modal from "react-bootstrap/Modal";
import {Button, Form} from "react-bootstrap";

const ModalYesNo = ({show, onHide, final, title, yestitle, notitle}) => {

    return (
        <Modal
            show={show}
            onHide={onHide}
            centered
            size='lg'
        >
            <Modal.Header>
                <Modal.Title>
                    {title}
                </Modal.Title>
            </Modal.Header>
            <Modal.Footer>
                    <Button variant="outline-secondary" onClick={onHide}>{notitle}</Button>
                    <Button variant="outline-danger" onClick={() => {onHide();final()}}>{yestitle}</Button>
            </Modal.Footer>
        </Modal>
    );
};

export default ModalYesNo;
