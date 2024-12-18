import React from "react";

const SearchLabel = ({text, searchText}) => {
    const point = text.toLowerCase().indexOf(searchText.toLowerCase())
  return(
    point != -1 && searchText != ''?
    <div>
        { point != 0 ? <label className='me-1'>{text.slice(0, point)}</label> : null}
        <label className=' fw-bold'>{text.slice(point, point + searchText.length)}</label>
        <label className='ms-1'>{text.slice(point + searchText.length)}</label>
    </div>
          :
    <label>{text}</label>
  )
}

export default SearchLabel