import store from '@/store';
import * as loaderMutations from '@/store/modules/loader/mutations';

export async function withLoader(func: () => Promise<void>): Promise<void> {
    store.commit(loaderMutations.types.SHOW_LOADER);
    try {
        await func();
    } finally {
        store.commit(loaderMutations.types.HIDE_LOADER);
    }
}
