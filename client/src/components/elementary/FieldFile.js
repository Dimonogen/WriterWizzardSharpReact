import ModalCreateObj from "../modals/ModalCreateObj";
import {CreateLinkObj} from "../../http/ObjAPI";
import {Button, Form, Image} from "react-bootstrap";
import iconCreate from "../../assets/icons8-addFile.png";
import iconView from "../../assets/icons8-viewFile.png";
import {MENU_ROUTE} from "../../utils/consts";
import iconLink from "../../assets/icons8-add-link.png";
import Box from "@mui/material/Box";
import {DataGrid} from "@mui/x-data-grid";
import React from "react";

const FieldFile = ({setValue, value, name, disabled}) => {

    let image;

    const selectFile = e => {
        image = e.target.files[0]
    }

    return (
        <div>
            <Form.Group className="mb-3 w-100">
                <Form.Label style={{textTransform: "capitalize"}}>{name}</Form.Label>
                <Form.Control
                    className=""
                    type="file"
                    onChange={selectFile}
                />
            </Form.Group>
        </div>
    )
}

export  default  FieldFile