import { ADD_CONTACTS_FROM_API } from "./action-types";
export const addContactsFromApi = contacts => ({ type: ADD_CONTACTS_FROM_API, payload: contacts });