import React, { useState } from 'react';
import { useNavigate } from 'react-router-dom';
const ProductCard = ({ product }) => {
  const navigate = useNavigate();

  const handleViewProduct = () => {
    navigate("/ProductOverView", { state: { product } });
  };
  
  return (
    <div className="bg-white rounded-lg shadow-md overflow-hidden transform hover:scale-105 transition-transform duration-300">
      <img src={product.image} alt={product.name} className="w-full h-48 object-cover" />
      <div className="p-4">
        <h3 className="text-lg font-semibold text-gray-800">{product.name}</h3>
        <p className="text-gray-600 mt-2">{product.price}</p>
      <button
          onClick={handleViewProduct}
          className = "mt-4 w-full text-white py-2 rounded-lg transition-colors duration-300 'bg-blue-500 hover:bg-blue-600">
          View Details
        </button>
      </div>
    </div>
  );
};

export default ProductCard;