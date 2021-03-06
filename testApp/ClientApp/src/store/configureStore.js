﻿import { applyMiddleware, combineReducers, compose, createStore } from 'redux';
import thunk from 'redux-thunk';
import { routerReducer, routerMiddleware } from 'react-router-redux';
import * as UsersReducer from './UsersReducer';

export default function configureStore(history, initialState) {
  const reducers = {
    User: UsersReducer.reducer
  };

  const middleware = [
    thunk,
    routerMiddleware(history)
  ];
  const rootReducer = combineReducers({
    ...reducers,
    routing: routerReducer
  });

  return createStore(
    rootReducer,
    initialState,
    compose(applyMiddleware(...middleware))
  );
}
