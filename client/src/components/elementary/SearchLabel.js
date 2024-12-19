import React from "react";

const SearchLabel = ({text, searchText}) => {
    const point = text.toLowerCase().indexOf(searchText.toLowerCase())
  return(
    point != -1 && searchText != ''?
    <div className=''>
        { point != 0 ? <span className='me-1'>{text.slice(0, point)}</span> : null}
        <span className=' fw-bold'>{text.slice(point, point + searchText.length)}</span>
        <span className='ms-1'>{text.slice(point + searchText.length)}</span>
    </div>
          :
    <label>{text}</label>
  )
}

export default SearchLabel