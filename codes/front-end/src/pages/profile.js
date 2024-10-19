import React, { useState, useEffect } from 'react';

function CustomerProfile() {
    const [customer, setCustomer] = useState({
        username: '',
        email: '',
        firstName: '',
        lastName: '',
        shippingAddress: '',
        billingAddress: '',
        primaryNumber: '',
        secondaryNumber: '',
        age: ''
    });
    const [error, setError] = useState(null);

    useEffect(() => {
        const loadCustomerData = () => {
            const storedData = localStorage.getItem('userInfo');
            if (storedData) {
                try {
                    setCustomer(JSON.parse(storedData));
                } catch (error) {
                    console.error('Failed to parse user info:', error);
                    setError('Failed to load profile information.');
                }
            } else {
                setError('Please log in to view profile information.');
            }
        };
    
        loadCustomerData();
    }, []);
    

    if (error) return <div>Error: {error}</div>;

    return (
        <div className="customer-profile">
            <h1>Customer Profile</h1>
            <div><strong>Username:</strong> {customer.username || "Not provided"}</div>
            <div><strong>Email:</strong> {customer.email || "Not provided"}</div>
            <div><strong>First Name:</strong> {customer.firstName || "Not provided"}</div>
            <div><strong>Last Name:</strong> {customer.lastName || "Not provided"}</div>
            <div><strong>Shipping Address:</strong> {customer.shippingAddress || "Not provided"}</div>
            <div><strong>Billing Address:</strong> {customer.billingAddress || "Not provided"}</div>
            <div><strong>Primary Phone Number:</strong> {customer.primaryNumber || "Not provided"}</div>
            <div><strong>Secondary Phone Number:</strong> {customer.secondaryNumber || "Not provided"}</div>
            <div><strong>Age:</strong> {customer.age || "Not provided"}</div>
        </div>
    );
}

export default CustomerProfile;
