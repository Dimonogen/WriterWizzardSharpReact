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
    return (
        <Modal
            show={show}
            onHide={onHide}
            centered
            size='lg'
        >
            <Modal.Header closeButton>
                <Modal.Title id="contained-modal-title-vcenter">
                    {title || 'Выберите или перетащите файл для обложки'}
                </Modal.Title>
            </Modal.Header>
            <Modal.Body>
                {//<input type="file" accept="image/*" onChange={e => image = e.target.files[0]} />
                     }
                <Form.Control
                    className="mt-3"
                    type="file"
                    onChange={selectFile}
                />
            </Modal.Body>
            <Modal.Footer>
                <Button variant="outline-danger" onClick={onHide}>Закрыть</Button>
                <Button variant="outline-success" onClick={final}>Добавить</Button>
            </Modal.Footer>
        </Modal>
    );
};

export default OpenImageFile;
