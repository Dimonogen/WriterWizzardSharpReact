import {Button} from "react-bootstrap";
import React, {useState} from "react";

const PopupTooltip = ({name, text}) => {

    const [ShowText, SetShowText] = useState(false)

  return(
      <div  className='position-relative'>
          <label  className='Mytooltip border-dark rounded-2 text-bg-dark ps-2 pe-2 fs-5 text-white'>{name}
              <Button className='pt-0 pb-0 ps-2 pe-2 mb-1 ms-1 me-1' onClick={() => SetShowText(!ShowText)}>
                  i</Button><label className={ShowText? '':'visually-hidden'}>{text}</label></label>
      </div>

  )
}

export default PopupTooltip