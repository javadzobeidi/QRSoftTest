import {base,post,get,put,http_delete} from './ApiBase'


export const  User={
    login:(data) => ({ url: `/user/login`,...base, ...post,data}),
    getUserInfo:() => ({ url: `/user`,...base, ...get}),
    
}


export const  Register={
    validate:(id,invCode) => ({ url: `/register/validate/${id}?inv=${invCode}`,...base, ...get}),
    register:(data) => ({ url: `/register`,...base, ...post,data}),
}

export const  Report={
    getUsersCountByType:() => ({ url: `/report/GetUsersCountByType`,...base, ...get}),
    
}
