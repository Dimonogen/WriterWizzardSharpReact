import AttributeComponent from "./AttributeComponent";
import Field from "./Field";
import React, {useEffect, useState} from "react";
import {Button, Image} from "react-bootstrap";
import eyeOpen from '../../assets/EyeOpen.svg';
import eyeClose from '../../assets/EyeClose.svg';

const AttributeValAndConfComponent = ({e, value, global}) => {

    //console.log(e, value, global);
    const [attribValue, SetAttribValue] = useState(value)

    const [custom, SetCustom] = useState({})

    const [showConf, SetShowConf] = useState(global.isEdit);

    useEffect(() => {
        SetValueAttr(value);
    }, [global, e, value])

    const SetValueAttr = (data) =>
    {
        let val = attribValue.value;
        data.value = val;
        SetAttribValue(data);
        let type = global.attribTypes.filter((e) => e.id == data.typeId )[0];
        SetCustom({isComplex: type.isComplex, type: type.type, name: data.name, typeId: data.typeId, regEx: type.regEx});
        global.setValue({value: data.value.toString(), name: data.name, typeId: data.typeId}, e.id);
    }

    const SetValueCur = (data, id) => {
        global.setValue({value: data.toString(), name: custom.name, typeId: custom.typeId}, id);
    }

    return(
        <div className="p-0 d-flex">
            <div className="W-100">
                {
                    showConf ? <AttributeComponent value={attribValue} setValue={SetValueAttr} isEdit={global.isEdit}
                                                   attrTypes={global.attribTypes}/> : null
                }
                <Field isComplex={custom.isComplex} SetModalD={global.SetModal} type={custom.type} setValue={SetValueCur}
                       value={attribValue.value} name={custom.name} minlen={1} maxlen={255} id={e.id} regex={custom.regEx} objId={global.objId}
                       placeholder="Не заполнено" nullable={false} disabled={!global.isEdit}/>
            </div>
            <div className="d-flex">
                <Button className="p-0 ms-2 mt-auto mb-auto me-0" variant="outline-secondary" onClick={() => SetShowConf(!showConf)}><Image className='m-0' height='32px' width='32px' src={showConf ? eyeClose : eyeOpen}/></Button>
            </div>

        </div>
    )
}

export default AttributeValAndConfComponent