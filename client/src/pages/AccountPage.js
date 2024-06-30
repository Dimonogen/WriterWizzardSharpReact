import '../CSS/anim.css'
import {useNavigate, useParams} from "react-router-dom";
import React, {useContext, useEffect, useState} from "react";
import {DeleteUserLogo, EditUserInfo, EditUserLogo, getInfo, getMyUserInfo} from "../http/userAPI";
import {ACCOUNT_ROUTE} from "../utils/consts";
import {Context} from "../index";
import AccountSideBar from "../components/account/AccountSideBar";
import AccountBasicSettings from "../components/account/AccountBasicSettings";
import {observer} from "mobx-react-lite";

const AccountPage = observer( ({}) => {
    const {user} = useContext(Context)
    const id = user.info.id;

    const location = window.location.pathname;

    const navigate = useNavigate()

    const [userV, SetUserV] = useState({'name':'', 'id':'', icon:"iconNotFound.png", title: 'test'})
    const [UserLogo, SetUserLogo] = useState('iconNotFound.png')

    const [books, setBooks] = useState([])

    /*useEffect(() => {

        getBooksAuthor(id).then(data => {setBooks(data); })
    }, [id])*/

    const deleteImage = () => {
        DeleteUserLogo().then(data => { SetUserLogo('iconNotFound.png')
            user.setIcon('')})
    }

    const [VisModalYN, setModalYN] = useState(false)

    return (
        <div className='d-flex'>
            <div className='W-100 MContent d-flex'>

                <div className='W-30 Block'>
                  <AccountSideBar/>
                </div>

                <div className='W-70 ms-3 Block '>
                    {
                        location === ACCOUNT_ROUTE?
                        <AccountBasicSettings/>
                            :
                        null
                    }
                </div>

            </div>


        </div>
    )

})

export default AccountPage;