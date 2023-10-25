import { useState } from 'react'
import userLogo from '../../assets/user.png'
import useLogin from './useLogin';
import { useForm, Controller } from "react-hook-form";
import { LoadingModal } from '../../components';
import ReCAPTCHA from "react-google-recaptcha";

function Login() {
 
  const { handleSubmit, watch, register, setValue, control, getValues, formState: { isValid, errors }, reset } =
  useForm({  defaultValues:{}});
  const [active,setActive]=useState(false)

  const [onSubmit ,loading,error] = useLogin();
 
  function onChange(value) {
  setActive(true);
  }
  
  return (
    <section class="bg-gray-50 	">
       
          <LoadingModal open={loading} backdrop={true} /> 

    <div class="flex flex-col items-center justify-center px-6 py-8 mx-auto md:h-screen lg:py-0">
        <div class="w-full bg-white rounded-lg shadow  md:mt-0 sm:max-w-md xl:p-0  ">
          <div className="flex items-center justify-center mt-4">
          <img className="w-16 h-16 mr-2 " src={userLogo} alt="logo" />

          </div>
          
            <div class="p-6 space-y-4 md:space-y-6 sm:p-8">
            {error &&   <div className="bg-red-500/60 max-w-lg m-auto rounded-lg px-4 py-2">{error}</div> }

                <h1 class="text-xl font-bold leading-tight tracking-tight text-gray-900 md:text-2xl ">
                    Sign in to your account
                </h1>
                <form onSubmit={handleSubmit(onSubmit)}  className="space-y-4 md:space-y-6">

                    <div>
                        <label for="userName" class="block mb-2 text-sm font-medium text-gray-900 ">User Name</label>
                        <input type="userName"  {...register("userName",{ required: true })}
                        name="userName" id="userName" class="bg-gray-50 border border-gray-300 text-gray-900 sm:text-sm rounded-lg focus:ring-primary-600 focus:border-primary-600 block w-full p-2.5 " placeholder="name@company.com" required="" />
                      {errors.userName  &&  <div className="bg-red-300/60 rounded-md  py-2 my-2 px-4 font-bold">UserName is Required</div> }
                    </div>
                    <div>
                        <label for="password" class="block mb-2 text-sm font-medium text-gray-900 ">Password</label>
                        <input type="password"   {...register("password",{ required: true })}
                         name="password" id="password" placeholder="••••••••" class="bg-gray-50 border border-gray-300 text-gray-900 sm:text-sm rounded-lg focus:ring-primary-600 focus:border-primary-600 block w-full p-2.5   " required="" />
                        {errors.password  &&  <div className="bg-red-300/60 rounded-md  py-2 my-2 px-4 font-bold">Password is Required</div> }

                    </div>
                    <div className="flex items-center justify-between">
                    <ReCAPTCHA
    sitekey="6Lc5gskoAAAAAHetro6x7xw5kimk1VffAX-zMsFf"
    onChange={onChange}
  />
                    </div>
            
                  <input disabled={active?false:true} type="submit" value="Submit" className={`w-full text-white  ${active?'bg-purple-600':'bg-gray-400'} hover:bg-primary-700 focus:ring-4 
                    focus:outline-none focus:ring-primary-300 font-medium rounded-lg text-sm px-5 py-2.5 text-center`} />
                    
                    
                </form>
            </div>
          

        </div>
    </div>
  </section>

  )
}

export default Login
