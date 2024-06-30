import {Button} from "react-bootstrap";
import ModalYesNoMy from "./modals/ModalYesNoMy";
import {useState} from "react";

const YesOfCouseButton = ({variant, final, title, props, classNames}) => {

    const [showModal, setShowModal] = useState(false)

    return (
        <div>
            <Button className={classNames} variant={variant} onClick={() => setShowModal(true)}>{props}</Button>
            <ModalYesNoMy show={showModal} onHide={() => setShowModal(false)} title={title} final={final}
                          notitle={'Отмена'} yestitle={'Да'}/>
        </div>
    )
}

export default YesOfCouseButton