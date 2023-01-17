import { useState } from 'react';
import FormInput from './FormInput';


const apiUrl= 'http://localhost:50874/api/Ingredients';

export default function Ingredient() {
  const [ingredientValues,setIngredientValues]= useState({
    Name:"",
    image:"",
    calories:""
  });

  const inputs=[
    {
      id : 1,
      name:"Name",
      type:"text",
      placeholder:"Enter name",
      label: "Name:"
    },
    {
      id : 2,
      name:"image",
      type:"text",
      placeholder:"Insert url image",
      label: "Image:"
    },
    {
      id : 3,
      name:"calories",
      type:"text",
      placeholder:"Insert calories",
      label: "Calories:"
    }
  ]

  const onChange = (e)=>{
    setIngredientValues({...ingredientValues,[e.target.name]: e.target.value})
  }


  const handelSubmit=(e)=>{
    e.preventDefault();
    
    fetch(apiUrl, {
      method: 'POST',
      body: JSON.stringify(ingredientValues),
      headers: new Headers({
      'Content-type': 'application/json; charset=UTF-8',
      'Accept':'application/json; charset=UTF-8'
      })
      })
      .then(res => {
      console.log('res=', res);
      return res.json()
      })
      .then(
      (result) => {
      console.log("fetch POST= ", result);
      console.log(result.Avg);
      },
      (error) => {
      console.log("err post=", error);
      });
    
   
    
    
  }

  

  return (
   <div>

    <form  style = {{margin: 80}} onSubmit={handelSubmit}>

      {inputs.map((input) => (
        <FormInput  
          {...input} 
          key={input.id}
          value={ingredientValues[input.name]} 
          onChange={onChange}
        />
      ))}

      <button type="submit">Create new ingredient</button>
      <button type="reset">reset Form</button> 
    </form>

   </div>
  )
}
