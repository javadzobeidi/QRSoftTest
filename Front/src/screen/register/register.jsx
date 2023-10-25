import { useState } from 'react'
import userLogo from '../../assets/user.png'
import { useForm, Controller } from "react-hook-form";
import { LoadingModal } from '../../components';
import useRegister from './useRegister';
import { useParams } from 'react-router-dom';
function Register() {
 
  const { handleSubmit, watch, register, setValue, control, getValues, formState: { isValid, errors }, reset } =
  useForm({  defaultValues:{}});

  const [onSubmit ,loading,error,success,errorPage] = useRegister();


  if (errorPage)
  return <ErrorPage />
  
  if (success)
  return <SuccessPage />
  
  return (
    <section class="bg-gray-50 	">
       
          <LoadingModal open={loading} backdrop={true} /> 

    <div class="flex flex-col items-center justify-center px-6 py-8 mx-auto md:h-screen lg:py-0">
        <div class="w-full bg-white rounded-lg shadow  md:mt-0 lg:max-w-6xl sm:max-w-md xl:p-0  ">
          <div className="flex items-center justify-center mt-4">
          <img className="w-16 h-16 mr-2 " src={userLogo} alt="logo" />

          </div>
          
            <div class="p-6 space-y-4 md:space-y-6 sm:p-8">

                <h1 class="text-xl font-bold leading-tight tracking-tight text-gray-900 md:text-2xl ">
                    Register
                </h1>
                <form onSubmit={handleSubmit(onSubmit)}  className="space-y-4 md:space-y-6 ">
               {error &&   <div className="bg-red-500/60 max-w-lg m-auto rounded-lg px-4 py-2">{error}</div> }
                  <div className="grid grid-cols-2   gap-4">
                    <div>
                        <label for="userName" className="block mb-2 text-sm font-medium text-gray-900 ">User Name</label>
                        <input type="text"  {...register("userName",{ required: true })}
                        name="userName" id="userName" className="outline-none bg-white border border-gray-300 text-black sm:text-sm rounded-lg focus:border-purple-500  w-full p-2.5 " placeholder="User Name" required="" />
                      {errors.userName  &&  <div className="bg-red-300/60 rounded-md  py-2 my-2 px-4 font-bold">UserName is Required</div> }
                    </div>
                    <div>
                        <label for="password" className="block mb-2 text-sm font-medium text-gray-900 ">Password</label>
                        <input type="password"   {...register("password",{ required: true })}
                         name="password" id="password" placeholder="••••••••" class="outline-none bg-white border border-gray-300 text-black sm:text-sm rounded-lg focus:border-purple-500  w-full p-2.5   " required="" />
                        {errors.password  &&  <div className="bg-red-300/60 rounded-md  py-2 my-2 px-4 font-bold">Password is Required</div> }

                    </div>

                    <div>
                        <label for="firstName" className="block mb-2 text-sm font-medium text-gray-900 ">First Name</label>
                        <input type="text"  {...register("firstName",{ required: true })}
                        name="firstName" id="firstName" className="outline-none bg-white border border-gray-300 text-black sm:text-sm rounded-lg focus:border-purple-500  w-full p-2.5 " placeholder="First Name" required="" />
                      {errors.firstName  &&  <div className="bg-red-300/60 rounded-md  py-2 my-2 px-4 font-bold">First Name is Required</div> }
                    </div>

                    <div>
                        <label for="lastName" className="block mb-2 text-sm font-medium text-gray-900 ">Last Name</label>
                        <input type="text"  {...register("lastName",{ required: true })}
                        name="lastName" id="lastName" className="outline-none bg-white border border-gray-300 text-black sm:text-sm rounded-lg focus:border-purple-500  w-full p-2.5 " placeholder="Last Name" required="" />
                      {errors.lastName  &&  <div className="bg-red-300/60 rounded-md  py-2 my-2 px-4 font-bold">Last Name is Required</div> }
                    </div>

                    <div>
                        <label for="email" className="block mb-2 text-sm font-medium text-gray-900 ">Email</label>
                        <input type="email"  {...register("email")}
                        name="email" id="email" className="outline-none bg-white border border-gray-300 text-black sm:text-sm rounded-lg focus:border-purple-500  w-full p-2.5 " placeholder="Email" required="" />
                      {errors.lastName  &&  <div className="bg-red-300/60 rounded-md  py-2 my-2 px-4 font-bold">Email  is Required</div> }
                    </div>

                    <div>
                        <label for="job" className="block mb-2 text-sm font-medium text-gray-900 ">Job</label>
                        <input type="text"  {...register("job")}
                        name="job" id="job" className="outline-none bg-white border border-gray-300 text-black sm:text-sm rounded-lg focus:border-purple-500  w-full p-2.5 " placeholder="Job" required="" />
                      {errors.lastName  &&  <div className="bg-red-300/60 rounded-md  py-2 my-2 px-4 font-bold">Job is Required</div> }
                    </div>

            
            <div className="col-span-2 flex items-center justify-center my-4">
                  <input type="submit" value="Submit" className="max-w-lg w-full text-white bg-purple-600 hover:bg-primary-700 focus:ring-4 
                    focus:outline-none focus:ring-primary-300 font-medium rounded-lg text-sm px-5 py-2.5 text-center" />

          </div>
                    
                  

                    </div>
                </form>
            </div>
          

        </div>
    </div>
  </section>

  )
}



const SuccessPage=()=>{
  return (
    <section class="bg-gray-50 	">
    <div class="flex flex-col items-center justify-center px-6  mx-auto md:h-screen lg:py-0">
        <div class="w-full bg-green-400 rounded-lg shadow  md:mt-0 lg:max-w-6xl sm:max-w-md xl:p-0  ">
          <div className="flex items-center justify-center  py-4">
          Thank you for your registration
          </div>
          

        </div>
    </div>
  </section>

  )
}


const ErrorPage=()=>{
  return (
    <section class="bg-gray-50 	">
    <div class="flex flex-col items-center justify-center px-6  mx-auto md:h-screen lg:py-0">
        <div class="w-full bg-red-400 rounded-lg shadow  md:mt-0 lg:max-w-6xl sm:max-w-md xl:p-0  ">
          <div className="flex items-center justify-center  py-4">
          Invalid Invitation Code
          </div>
          

        </div>
    </div>
  </section>

  )
}

export default Register
