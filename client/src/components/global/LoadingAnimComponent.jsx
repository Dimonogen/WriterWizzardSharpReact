import {Spinner} from "react-bootstrap";
import React from "react";

const LoadingAnimComponent = () => {
  return (
      <div className='MContent d-flex justify-content-center align-items-center'>
          <div className='d-flex flex-column'>
              <div className='d-flex justify-content-center'>
                  <Spinner style={{height:'64px', width:'64px'}} animation={'border'}/>
              </div>

              Загрузка...
          </div>

      </div>
  )
}

export default LoadingAnimComponent