import { useApi,User } from '../../Api';

import {useState} from 'react'
import {useStore} from '../../store'
import { useNavigate } from 'react-router-dom';

const useLogin = () => {

    const [loading, callApi] = useApi();
    const [error,setError]=useState('');
    const store=useStore();
    const navigate=useNavigate();


  const onSubmit = async (data) => {
   var result=await  callApi(User.login(data));
   console.log("Result",result)
   if (!result.success)
   {

    if (result.httpCode===429)
    {
      setError("IP Blocked")
      return;
    }

    setError(result.message);
    console.log("Message");
    return;
   }


   store.setAuthenticate(true)
   navigate("/")

}

return [onSubmit,loading,error]

}

export default useLogin;