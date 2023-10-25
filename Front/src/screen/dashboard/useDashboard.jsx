import { useStore } from '../../store';
import { useApi,User,Report } from '../../Api';
import {useEffect, useState} from 'react'
import { useParams,useNavigate } from "react-router-dom";

const useDashboard = () => {
    const [loading, callApi] = useApi();
    const [error,setError]=useState('')
const [reports,setReports]=useState([]);
    const navigate=useNavigate();


const store=useStore();

    useEffect(()=>{
      getUserInfo();
    },[])

    useEffect(()=>{
      if (store.user.role==="admin")
      {
        getUserReports();
      }
    },[store.user])


  const getUserInfo=async ()=>{
   var result=await callApi(User.getUserInfo());
   if (!result.success)
   {
    store.setAuthenticate(false);
    navigate("/login")
    return;
   }

    store.setUser(result.model)
  }

  const getUserReports=async()=>{
    var result=await callApi(Report.getUsersCountByType());
    if (!result.success)
    {
      setError(result.message);
     return;
    }

    setReports(result.model.list)

  
  }


  const signout=async ()=>{
    
    store.setAuthenticate(false);
    store.setUser({});
    navigate("/");
  }


return [error,loading,store.user,reports,signout]

}

export default useDashboard;