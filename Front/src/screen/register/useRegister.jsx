import { useApi,Register } from '../../Api';

import {useEffect, useState} from 'react'
import {useStore} from '../../store'
import { useParams } from 'react-router-dom';

const useRegister = () => {

    const [loading, callApi] = useApi();
    const [error,setError]=useState('');
   const [success,onSuccess]=useState(false);
   const [errorPage,setErrorPage]=useState(false);

    const {registerId,inv}=useParams();
   
    useEffect(()=>{
     validateRefferalLink();
    },[registerId])

    const validateRefferalLink=async()=>{
      if (registerId===undefined || registerId==='')
           return;  

    var result=await  callApi(Register.validate(registerId,inv));
    if (!result.success)
    {
      setErrorPage(true)
    }


      
    }

  const onSubmit = async (data) => {

    data.registerId=registerId;
    data.invCode=inv;
   var result=await  callApi(Register.register(data));
   console.log("Result",result)
   if (!result.success)
   {
    setError(result.message);
    return;
   }

   onSuccess(true);

}

return [onSubmit,loading,error,success,errorPage]

}

export default useRegister;