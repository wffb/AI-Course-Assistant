import React, { createContext, useState, useEffect } from "react";
import jwt_decode from "jwt-decode";
const UserContext = createContext();

const STATUS_BAD_REQUEST = 400;
const STATUS_CREATED = 201;
const STATUS_FORBIDDEN = 403;
const STATUS_ERROR = 401;
const STATUS_SET_UP_OTP = 202;
const STATUS_INVALID_OTP = 200;
const backendUrl = process.env.REACT_APP_BACKEND_URL;
const getSavedTokens = () => {
  const savedTokens = localStorage.getItem("userTokens");
  return savedTokens ? JSON.parse(savedTokens) : null;
};

export const UserProvider = ({ children }) => {
  const initialTokens = getSavedTokens();

  let decodedUser = null;
  if (
    initialTokens &&
    typeof initialTokens.access === "string" &&
    initialTokens.access.split(".").length === 3
  ) {
    try {
      decodedUser = jwt_decode(initialTokens.access);
    } catch (err) {
      console.error("Failed to decode the token", err);
    }
  }

  let [user, setUser] = useState(decodedUser);
  let [userTokens, setUserTokens] = useState(initialTokens);
  let [loading, setLoading] = useState(true);
  let [error, setError] = useState(null);
  const [userRole, setUserRole] = useState({});
  const handleError = (errorMessage) => {
    setError(errorMessage);
  };

  const loginUser = async (username, password, otp) => {
    try {
      const response = await fetch(backendUrl + "user/login/", {
        method: "POST",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify({ username, password ,otp}),
      });

      const data = await response.json();
      switch (response.status) {
        case STATUS_CREATED:
          localStorage.setItem(
            "userTokens",
            JSON.stringify({
              access_token: data.access_token,
              refresh_token: data.refresh_token,
            })
          );
          localStorage.setItem('userInfo', JSON.stringify(data.user_info));
          setUser({
            access_token: data.access_token,
            refresh_token: data.refresh_token,
          });
          setUserTokens({
            access_token: data.access_token,
            refresh_token: data.refresh_token,
          });
          return [201,true];

        case STATUS_BAD_REQUEST:
          handleError("Invalid Username, please check your username!");
          break;

        case STATUS_FORBIDDEN:
          handleError(
            "Invalid credentials."
          );
          break;

        case STATUS_SET_UP_OTP:
          // console.log("Response data:", data);
          const qr_code = data.qr_code
          
          return [202,qr_code];

        case STATUS_INVALID_OTP:
          handleError(
            "Invalid OTP"
          )
          break;
        
        case STATUS_ERROR:
          handleError(
            'Account not activated, please verify your email first'
        )
          break;
        default:
          handleError("Log in failed, please try again later.");
          break;
      }
    } catch (error) {
      handleError(
        "Log in failed due to network or server issues, please try again later."
      );
    }
    return [404,false];
  };




  
  const registerUser = async (firstname, lastname, username, password, email) => {
    try {
      const first_name = firstname;
      const last_name = lastname;
      const response = await fetch(backendUrl+"user/manage/", {
        method: "POST",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify({ first_name, last_name, username, password, email }),
      });
      
      switch (response.status) {
        case STATUS_CREATED:
          console.log("Registered successfully");
          return true;

        case STATUS_BAD_REQUEST:
          handleError("Username already taken");
          break;
        case STATUS_ERROR:
          handleError("Please enter a valid email address");
          break;
        default:
          handleError("Sign up failed, please try again later.");
          break;
      }
    } catch (error) {
      handleError(
        "Sign up failed due to network or server issues, please try again later."
      );
    }
    return false;
  };





  const logoutUser = () => {
    setUserTokens(null);
    setUser(null);
    localStorage.removeItem("userTokens");
    localStorage.removeItem("userRole");
  };

  useEffect(() => {
    setLoading(false);
  }, [userTokens]);

  

  return (
    <UserContext.Provider
      value={{
        user,
        setUser,
        userTokens,
        setUserTokens,
        error,
        registerUser,
        loginUser,
        logoutUser,
        setError,
        userRole,
        setUserRole,
      }}
    >
      {loading ? null : children}
    </UserContext.Provider>
  );
};

export default UserContext;
