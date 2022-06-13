import React from 'react'
import { pets, pets as petsArray } from './Data_test';
import { useCallback, useEffect, useRef, useState } from 'react';

function Test() {
  const [pets, setPets] = useState(petsArray);
  console.log(pets);
  return (
    <div>CDSACASDCSDCS</div>
  )
}

export default Test