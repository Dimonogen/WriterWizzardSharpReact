import FieldObj from "./FieldObj";
import FieldText from "./FieldText";
import FieldObjGrid from "./FieldObjGrid";
import FieldFile from "./FieldFile";

const Field = ({type, objId, isComplex, regex, setValue, value, name, minlen, maxlen, id, placeholder, nullable, disabled, SetModalD}) => {

    return (
        regex == 'Reference' ? <FieldObjGrid type={type} name={name} disable={disabled} objId={objId} SetMSelectD={SetModalD}/>
            :
        isComplex ?
        <FieldObj SetModalD={SetModalD} type={type} setValue={setValue} value={value} name={name} number={id} disable={disabled}
        />
        :
        type == 3 ?
        <FieldFile setValue={setValue} value={value} name={name} disabled={disabled}/>
            :
        <FieldText setValue={setValue} value={value} name={name} id={id} minlen={minlen} maxlen={maxlen}
                   placeholder={placeholder} nullable={nullable} disabled={disabled} rows={1} />
    )
}

export default Field