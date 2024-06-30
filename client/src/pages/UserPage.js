import '../CSS/anim.css'
import {useNavigate, useParams} from "react-router-dom";
import {
    Button,
    Image,
    Row,
} from "react-bootstrap";
import React, {useContext, useEffect, useState} from "react";
import {DeleteUserLogo, EditUserLogo, getInfo} from "../http/userAPI";
import {ACCOUNT_ROUTE} from "../utils/consts";
import {Context} from "../index";
import ModalYesNoMy from "../components/modals/ModalYesNoMy";
import OpenImageFileMy from "../components/modals/OpenImageFileMy";

const UserPage = ({}) => {
    const {id} = useParams()
    const {user} = useContext(Context)
    const navigate = useNavigate()

    const [userV, SetUserV] = useState({'name':'', 'id':'', icon:"iconNotFound.png", title: 'test'})
    const [UserLogo, SetUserLogo] = useState('iconNotFound.png')

    const [books, setBooks] = useState([])

    useEffect(() => {
        getInfo(id).then(data => {
            SetUserV(data)
            if(data.icon)
            {
                SetUserLogo(data.icon);
            }
            }
        )
    }, [id])

    const [imageVisible, setImageVisible] = useState(false)
    //const [image_src, setImageSrc] = useState( '')
    const [fileImg, setFileImg] = useState(null)

    const addImage = (img) =>
    {
        //console.log(img)
        if(img) {
            setFileImg(img)
            const formData = new FormData();
            formData.append('icon', img);
            EditUserLogo(formData).then(data => {SetUserLogo(data);
                user.setIcon(data)});
        }
        //console.log("image_src" + image_src)
    }

    const deleteImage = () => {
        DeleteUserLogo().then(data => { SetUserLogo('iconNotFound.png')
            user.setIcon('')})
    }

    const [VisModalYN, setModalYN] = useState(false)

    return (
        <div className='d-flex ms-4 me-4'>

            <OpenImageFileMy show={imageVisible} onHide={(image) => {
                setImageVisible(false)
                addImage(image)
            }} title='Выберите или перетащите картинку для аватарки'/>

            <ModalYesNoMy show={VisModalYN} onHide={() => {setModalYN(false)}}
                        title='Вы действительно хотите удалить аватарку?' final={deleteImage} notitle='Отменить' yestitle='Удалить аватарку'/>

            <div className='W-100'>
                <div className='W-100'>
                    <Image className='img-fluid' src={process.env.REACT_APP_STATIC_URL + 'defaultUB.jpg'}/>
                </div>

                <div className='W-100 Block'>
                    {
                        id == user?.info?.id
                        ?
                        <div style={{
                            overflow: 'hidden',
                            position: "absolute",
                            top: '-170px',
                            left: '40px',
                            width: '200px',
                            height: '200px',
                            borderRadius: '10px',
                            zIndex:'10'
                        }}>
                            <div style={{position:'absolute', top:'-00px'}}>
                                <div className='anim1'
                                     style={{position: "absolute", width: '200px', height: '400px', top: '-00px'}}
                                    //onMouseEnter={()=>SetShowButton({display: 'block'})}
                                    //onMouseLeave={()=>SetShowButton({display: 'none'})}

                                >
                                    <div className='' style={{
                                        position: 'absolute',
                                        backgroundColor: 'black',
                                        width: '200px',
                                        height: '100px',
                                        top: '200px',
                                        opacity: '50%'
                                    }}>

                                    </div>
                                    <div className='' style={{position: 'absolute', top: '210px', left: '40px'}}>
                                        <Button className='' onClick={() => {setImageVisible(true)} } >Select logo</Button>
                                        <Button className='mt-2' onClick={() => setModalYN(true)}>Delete logo</Button>
                                    </div>
                                </div>
                            </div>
                        </div>
                            :
                            null
                    }

                    <Image

                        style={{position:"absolute", top:'-170px', left:'40px', border:'5px white solid', borderRadius: '10px', zIndex:'0'}}
                        width={200} height={200} src={process.env.REACT_APP_STATIC_URL+UserLogo}/>


                    <div className='text-white' style={{position:'absolute', left:'300px', top:'-80px',textShadow: '1px 1px 2px black' }}>
                        <h3>{userV.name}</h3>
                        <h5>{userV.title}</h5>
                    </div>
                    <div className='text-white'>
                        .
                    </div>
                    <div style={{position:"absolute", top:'-60px', right:'50px'}}>
                    {
                        id == user?.info?.id
                            ?
                        <Button variant="light" onClick={() => navigate(ACCOUNT_ROUTE)}>Редактировать профиль</Button>
                            :
                        <Button variant="light" onClick={() => navigate("privatemessage")}>Сообщение</Button>
                    }
                    </div>
                </div>
                <div className='W-100 mt-3 d-flex'>
                    <div className='W-25 me-3'>
                        <div className='Block'>
                            Заходил 1 час назад
                        </div>
                        <div className='Block mt-3'>
                            Регистрация: ...
                        </div>
                    </div>
                    <div className='W-50 me-3'>
                        <div className='Block'>

                        </div>

                        <div className='mt-3 Block'>
                            О себе
                            <div>
                                {userV.description}
                            </div>
                        </div>
                    </div>
                    <div className='W-25 Block'>
                        Друзья
                    </div>
                </div>

            </div>


        </div>
    )

}

export default UserPage;