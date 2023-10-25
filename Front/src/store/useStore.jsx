import { create } from 'zustand'
import { persist, createJSONStorage } from 'zustand/middleware'



const useStore = create(
  persist(
    (set, get) => ({
      isAuthenticate:false,
      user: null,
      setAuthenticate: (data) => set({ isAuthenticate:data }),
      
      setUser: (data) => set({ user:data }),

    }),
    {
      name: 'qrhsoft-storage', // name of the item in the storage (must be unique)
      storage: createJSONStorage(() => localStorage), // (optional) by default, 'localStorage' is used
    }
  )
)


export default useStore;