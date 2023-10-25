import {Login} from './screen/login'
import { Route, Routes } from "react-router-dom";
import {useStore} from './store'
import {
  BrowserRouter as Router,
} from "react-router-dom";
import { Dashboard } from './screen/dashboard';
import { Register } from './screen/register';


function App() {

  const store=useStore();



    
  const MainRoute=()=>{
    if (!store.isAuthenticate)
    {
    return (<>
        <Route path="/" element={<Login />} /></>)
    }

    return (<>
      <Route path="/" element={<Dashboard />} /></>      
      )
      
 
  }

  
  return (
    <Router>
    <Routes>
    {MainRoute()}

    <Route path="/register/:registerId/inv/:inv" element={<Register />} />
   <Route path="/register" element={<Register />} />

   
    </Routes>
  </Router>

  )
}

export default App
