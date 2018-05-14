import api from "../../../api"
import mutations from "./mutation_types"
import actions from "./action_types"

export default
{
    state: {
        init_request_pending: false,
        message_request_pending: false,
        page: 0,
        pageSize: 10,
        theme: {
            messages: []
        }
    },
    mutations: {
        [mutations.THEME_REQUEST]: (state) => {
            state.request_pending = true;
        },
        [mutations.THEME_SUCCESS]: (state, data) => {
            state.request_pending = false;
            state.theme = data;
        },
        [mutations.MESSAGE_REQUEST]: (state, { page, pageSize }) => {
            state.message_request_pending = true;
            state.page = page;
            state.pageSize = pageSize;
        },
        [mutations.MESSAGE_SUCCESS]: (state, data) => {
            state.message_request_pending = false;
            state.theme.messages = data;
        }
    },
    actions: {
        [actions.GET_THEME]: ({ commit }, id) => {
            commit(mutations.THEME_REQUEST);
            api.get('theme/' + id).then(response => {
                commit(mutations.THEME_SUCCESS, response.data);
            });
        },
        [actions.GET_MESSAGES]: ({ commit }, { id, page, pageSize }) => {
            commit(mutations.MESSAGE_REQUEST, { page, pageSize });
            var url = 'theme/' + id + '/messages';
            api.get(url,
                {
                    params:
                    {
                        page: page,
                        pageSize: pageSize
                    }
                }
            ).then(response => {
                commit(mutations.MESSAGE_SUCCESS, response.data);
            });
        }
    },
    getters: {
        getTheme: state => {
            return state.theme;
        },
        getMessages: state => {
            return state.theme.messages;
        },
        getPageOptions: state => {
            var page = state.page;
            var pageSize = state.pageSize;
            return { page, pageSize };
        }
    }
}