


export const base={

baseURL:'http://localhost:5001'
       ,headers:{'Content-Type':'application/json',withCredentials: true}}


    export   const get={method:'get',withCredentials: true}
     export  const post={method:'post',withCredentials: true}
     export  const put={method:'put'}
     export const http_delete={method:'delete'}

     if (!process.env.NODE_ENV || process.env.NODE_ENV === 'development') {
      // dev code
  } else {
      // production code
  }

