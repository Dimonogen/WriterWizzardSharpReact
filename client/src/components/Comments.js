import {Button, ButtonGroup, DropdownButton, Dropdown, Form} from "react-bootstrap";
import Comment from "./Comment";
import React, {useContext, useEffect, useState} from "react";
import {createBookComment, fetchBookComments} from "../http/BookAPI";
import {Context} from "../index";

const Comments = ({ObjType, ObjId}) => {

    const {user} = useContext(Context)

    const [comments, SetComments] = useState([])

    const sort_vars = [{'title':'времени, по убыванию', 'key':'0'},
        {'title':'времени, по возрастанию', 'key':'1'},
        {'title':'популярности, по убыванию', 'key':'2'},
        {'title':'популярности, по возрастанию', 'key':'3'}
    ];

    const [sortSel, SetSortSel] = useState(sort_vars[0].key)
    const [c_text, SetC_text] = useState('')

    useEffect(()=>{
        Update()
    }, [ObjId])

    function Update(){
        if(ObjType === 'book') {
            fetchBookComments(ObjId).then(data => SetComments(data))
        }
    }

    function SendComment() {
        if(c_text == '')
        {
            alert('Введите комментарий.')
            return;
        }
        if(c_text.length < 10)
        {
            alert('Комментарий должен быть не меньше 10 символов.')
            return;
        }
        let values = {c_text: c_text, user: user.info.id, book: ObjId}
        //console.log(values)
        createBookComment(values).then(data => {//console.log(data);
            Update(); SetC_text('')})
    }

    return (
        <div>
            <div className='fw-bold p-2 ps-3'>
                Комментарии
            </div>
            <div className='d-flex p-2 ps-3'>
                <div className='mt-2'>Сортировать по</div>
                <DropdownButton
                    as={ButtonGroup}
                    id={`dropdown-sort-comments`}
                    variant='outline-secondary'
                    title={sort_vars[sortSel].title}
                    className='ms-2'
                >
                    {
                        sort_vars.map(e =>
                            <Dropdown.Item onClick={() => SetSortSel(e.key)} key={e.key} eventKey={e.key} active={sortSel === e.key}>{e.title}</Dropdown.Item>
                        )
                    }
                </DropdownButton>
            </div>
            {
                user.isAuth ?
                <Form className='mt-3'>
                    <Form.Group className="mb-3" controlId="exampleForm.ControlTextarea1">
                        <Form.Control value={c_text} onChange={e => SetC_text(e.target.value)} placeholder='Написать комментарий...' as="textarea" rows={2} />
                    </Form.Group>
                    <Button variant='alter-dark' onClick={() => SendComment()}>Отправить</Button>
                </Form>
                :
                <div>
                    Оставлять комментарии могут только зарегистрированные пользователи.
                </div>
            }

            {
                comments.length > 0
                ?
                comments.map(comm =>
                    <Comment ObjId={ObjId} Update={Update} key={comm.id} com={comm}/>
                )
                :
                null
            }
        </div>
    )
}

export default Comments;