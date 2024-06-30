import { observer } from 'mobx-react-lite'
import React, {useContext, useEffect, useState} from 'react'
import {Button, Card, Col, Form, Image, OverlayTrigger, Tooltip} from 'react-bootstrap'
import {NavLink, useNavigate} from "react-router-dom"
import {BOOK_ROUTE, USER_ROUTE} from '../utils/consts'
import {DateFormat, DateTimeDiffer} from "../utils/functions";
import {Context} from "../index";
import {
    createBookComment,
    deleteBookComment,
    fetchBookCommentChild,
    fetchBookComments,
    setCommentHaveChild
} from "../http/BookAPI";

const Comment = observer( ({com, Update, ObjId, isCreated}) => {
    const navigate = useNavigate()

    useEffect(()=>{

    }, [])

    //console.log(com)

    const {user} = useContext(Context)

    const [isEdit, SetIsEdit] = useState(isCreated || false)
    const [c_text, SetC_text] = useState(com.text)
    const [haveActiveChild, SetHaveActiveChild] = useState(false)
    const [treeOpen, SetTreeOpen] = useState(false)
    const [childs, SetChilds] = useState([])

    function CreateChild() {
        SetHaveActiveChild(true)
    }

    function OpenTree(){
        fetchBookCommentChild(com.id).then(data => SetChilds(data)).finally(SetTreeOpen(true))
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
        let values = {c_text: c_text, user: user.info.id, book: ObjId, level: com.level, parentId: com.parentId}
        if(com.id != null)
        {
            values.id = com.id;
        }
        //console.log(values)
        createBookComment(values).then(data => {//console.log(data);
            Update(); SetIsEdit(false)})
    }

    function deleteComm(){
        deleteBookComment(com.id).then(data => {
            Update();
        })
    }

    function UpdateThis(){
        if(!com.haveChild) {
            setCommentHaveChild(com.id, {'haveChild':true}).then((data) => {
                com.haveChild = data.haveChild
                SetHaveActiveChild(false)
                OpenTree()
                Update()
            })
        }
        else {
            SetHaveActiveChild(false)
            OpenTree()
            Update()
        }
    }

    //console.log(com)
    let com_data = new Date(com.createdAt)//com_data.toString()

    return (
        <div className='m-2 me-0'>
            <div className='d-flex W-100'>
            <Image width={48} height={48} src={process.env.REACT_APP_STATIC_URL + (com.user.icon ? com.user.icon : 'iconNotFound.png')} className='rounded-circle me-2'/>

            <div className='W-100'>
                <div>
                    <NavLink className='alter' to={USER_ROUTE+'/'+com.user.id}>{com.user.name}</NavLink>
                    <OverlayTrigger delay='100'
                                    overlay={<Tooltip>{DateFormat(com_data)}</Tooltip>}
                                    >
                        <span className='ms-1'>{DateTimeDiffer(com_data, Date.now())}</span>
                    </OverlayTrigger>

                </div>
                {
                    isEdit
                    ?
                        <Form className='mt-3'>
                            <Form.Group className="mb-3" controlId="exampleForm.ControlTextarea1">
                                <textarea className='text-area1' value={c_text} onChange={e => SetC_text(e.target.value)} placeholder='Написать комментарий...' as="textarea"  />
                            </Form.Group>

                        </Form>
                        :
                    com.text.split('\n').map(e =>
                    e.length == 0
                    ?
                        <br key={Math.random()}/>
                        :
                        <p key={Math.random()}>{e}</p>
                    )}

                <div className='mt-2 W-100 d-flex'>
                    {
                        isEdit
                        ?
                            <Button size='sm' variant='alter-dark' onClick={() => SendComment()}>Сохранить</Button>
                            :
                        haveActiveChild ? null :
                            <Button size='sm' variant='alter-darken' onClick={() => CreateChild()}>Ответить</Button>
                    }

                    {
                        com.user.id == user.info.id
                        ?
                        <div className='ms-auto me-2'>
                            <div style={{position: 'relative', top:'0px', left:'0px'}}>
                                {
                                    isEdit
                                    ?
                                        null
                                    :
                                        <Button size='sm' variant='outline-alter-darken' onClick={()=>SetIsEdit(true)}>Редактировать</Button>
                                }

                                <Button className='ms-2' size='sm' variant='outline-danger' onClick={()=>deleteComm()}>Delete</Button>
                            </div>
                        </div>
                        :
                        null
                    }
                </div>

            </div>
            </div>

            {
                com.haveChild?
                    treeOpen ?
                        <Button variant='outline-secondary' size='sm' className='mt-1' onClick={() => SetTreeOpen(false)}>-</Button>
                        :
                        <Button variant='outline-secondary' size='sm' className='mt-1' onClick={()=>{OpenTree()} }>+ Расскрыть ветвь</Button>
                    :
                    null
            }

            <div className='ms-2' id='dots' style={{borderLeft:'1px dashed var(--c-ligh-darken)'}}>
                {
                    haveActiveChild ?
                        <Comment com={{text:'', user: {icon:user.icon, id:user.info.id, name:user.info.name}, createdAt:Date.now(),
                            level:com.level+1, parentId: com.id}} isCreated={true} ObjId={ObjId} Update={UpdateThis}/>
                        : null
                }
                {
                    treeOpen ?
                        childs.map(e =>
                            <Comment com={e} ObjId={ObjId} Update={Update} />
                        )
                    : null
                }

            </div>

        </div>
    )
})

export default Comment