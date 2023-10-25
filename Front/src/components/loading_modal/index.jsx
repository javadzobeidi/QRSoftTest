import { ClipLoader } from "react-spinners"
import { Dialog, Transition } from "@headlessui/react";

const LoadingModal=({open,backdrop=false})=>{
    return (
        <Dialog
        open={open}
        style={{ zIndex: 50 }}
        className={`fixed  inset-0 flex justify-center items-center  w-screen h-full    overflow-auto `}
        onClose={() => {}}
      >

        {backdrop && <div className="h-screen w-full inset-0 absolute bg-gray-100/60"></div> }
<div style={{ height: 50 }} className="mx-2 mb-1 px-12 py-12 flex justify-center items-center bg-white drop-shadow-2xl rounded-2xl">
<div className="p-4 mb-4 text-sm   "/>
  <span className="px-4">Please Wait...</span>
      <ClipLoader size={25} color={'red'} />
    </div>
    </Dialog>


    )
}

export default LoadingModal;