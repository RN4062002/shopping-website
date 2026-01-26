import React, { createContext,useContext,useState } from "react";
const AuthContext = createContext();
import {loginApi,registerApi} from "../api/AuthApi";


export const AuthContextProvider = ({children}) =>{
    const [user,setUser] = useState(null);
    const [loader,setLoader] = useState(false);

    const register = async(formData) =>
    {
        try
        {
            const payload = {
                firstName: formData.firstName,
                lastName: formData.lastName,
                userName: formData.userName,
                email: formData.email,
                password: formData.password
            };
            const res = await registerApi(payload);
            console.log(res);

        return true;
        }
        catch(error)
        {
            console.log(error);
        }
    }

    const login = async(userData,passData) => {
        try
        {
            setLoader(true);
            const data = await loginApi(userData,passData);
            console.log(data);
            localStorage.setItem("token",data.token);
            setUser(data.user);
           setLoader(false);

        }
        catch(error)
        {
            console.log(error);
        }
    };

    const logout = () => {
    localStorage.removeItem("token");
    setUser(null);
    setLoader(false);
    return true;
  };

    return (
        <AuthContext.Provider value={{user,login,logout,loader,register}}>
            {children}
        </AuthContext.Provider>
    );
}

export const useAuth = () => useContext(AuthContext);