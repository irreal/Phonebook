import { ADD_CONTACTS_FROM_API } from "../actions/action-types";

const initialState = {
    contacts: []
};
const rootReducer = (state = initialState, action) => {
    switch (action.type) {
        case ADD_CONTACTS_FROM_API:
            return { ...state, contacts: [...state.contacts, action.payload] };
        default:
            return state;
    }
}
export default rootReducer;