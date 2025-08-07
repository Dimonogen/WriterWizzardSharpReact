import {Button} from "react-bootstrap";
import React, {useState} from "react";

const PopupTooltip = ({name, text}) => {

    const [ShowText, SetShowText] = useState(false)

  return(
      <div  className='position-relative'>
          <div className='Mytooltip border-dark rounded-2 text-bg-dark text-start d-flex' style={{width: '200px'}}>
              {
                  ShowText ?
                      <label className={' W-100 fs-6 ms-1'}>{text}</label>
                      :
                      <label  className='W-100  ps-2 pe-2 fs-5 text-white'>{name}</label>
              }

              <div className='pt-0 pb-0 ps-2 pe-2 mb-1 ms-1 me-1 mt-1 border-1 rounded-1' style={{backgroundColor: "blue"}} onClick={() => SetShowText(!ShowText)}>
                      i</div>
          </div>

      </div>

  )
}

export default PopupTooltip

/*
last version
<label  className='W-100  ps-2 pe-2 fs-5 text-white'>{name}
              <Button className='pt-0 pb-0 ps-2 pe-2 mb-1 ms-1 me-1' onClick={() => SetShowText(!ShowText)}>
                      i</Button>
              <label className={(ShowText? '':'visually-hidden') +' W-100 fs-6'}>{text}</label></label>
 */