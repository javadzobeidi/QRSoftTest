import { useState } from 'react'
import userLogo from '../../assets/user.png'
import useDashboard from './useDashboard';
import { useForm, Controller } from "react-hook-form";
import { LoadingModal } from '../../components';
function Dashboard() {
 


  const [error ,loading,user,reports,signout] = useDashboard();
 
  return (
    <section class="bg-gray-50 	">
       
          <LoadingModal open={loading} backdrop={true} /> 

    <div class="flex flex-col items-center justify-center px-6 py-8 mx-auto md:h-screen lg:py-0">
        <div class="w-full bg-white rounded-lg shadow  md:mt-0 sm:max-w-md xl:p-0  ">
          <div className="flex items-center justify-center mt-4">
          <img className="w-16 h-16 mr-2 " src={userLogo} alt="logo" />

          </div>
          
            <div class="p-6 space-y-4 md:space-y-6 sm:p-8">


                {user  && 
                <div>
                <h1 class="text-xl font-bold leading-tight tracking-tight text-gray-900 md:text-2xl ">
                {user.givenName} as {user.role} sign in
                </h1>
                </div>
                
                }
                
                {reports.map((item,index)=>{
                    return (
                        <div className='bg-slate-300 shadow-lg rounded-lg p-3'>{item.role}
                        <span className='bg-green-500 rounded-full mx-4 px-2 py-1'>{item.count}</span>
                        </div>
                    )
                })}
                  
                  
                
                  <input type="button" onClick={signout} value="Sign out" className="w-full text-white bg-purple-600 hover:bg-primary-700 focus:ring-4 
                    focus:outline-none focus:ring-primary-300 font-medium rounded-lg text-sm px-5 py-2.5 text-center" />
              
            </div>
          

        </div>
    </div>
  </section>

  )
}

export default Dashboard;
