import {Form} from "react-bootstrap";
import {useEffect, useState} from "react";

const AttributeComponent = ({value, setValue, isEdit, attrTypes}) => {

    useEffect(()=>{
        //console.log(value)
        SetName(value.name);
        SetType(value.typeId);
        //setValue({number: value.number, name: value.name, typeId: value.typeId}, value.number);
    }, [value])

    const [name, SetName] = useState(value.name);
    const [type, SetType] = useState(value.type);

    const SendChange = (nameF, typeF) => {
        //console.log({name: nameF, type: typeF});
        setValue({number: value.number, name: nameF, typeId: typeF}, value.number)
    }

    return (
        <div>
            <Form.Group className='mb-3 W-100'>

                <Form.Control
                    value={name}
                    as={"input"}
                    className={""}
                    onChange={e => {SetName(e.target.value); SendChange(e.target.value, type)}}
                    placeholder={"Название поля"}
                    disabled={!isEdit}
                />

                <div className='d-flex W-100 mt-2'>
                    <Form.Label className='mt-auto ms-2 me-3'>
                        Тип
                    </Form.Label>
                    <Form.Select value={type} onChange={(e)=>{SetType(Number(e.target.value));
                        SendChange(name, Number(e.target.value))}}>
                        {
                            attrTypes.map(e =>
                            <option key={e.id} value={e.id}>{e.name}</option>
                            )
                        }
                    </Form.Select>
                </div>

            </Form.Group>
        </div>
    )
}

export default AttributeComponent