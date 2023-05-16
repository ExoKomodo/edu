import { describe, it, expect, vi, type Mock } from 'vitest';

import { shallowMount } from '@vue/test-utils';
import Navbar from '../Navbar.vue';

import * as auth0 from '@auth0/auth0-vue';

interface Auth0Mock {
  useAuth0: Mock<any[], any>
}

vi.mock('@auth0/auth0-vue');

describe('Navbar', () => {
  it('renders properly', () => {
    ((auth0 as unknown) as Auth0Mock).useAuth0 = vi.fn().mockReturnValue({});
    const wrapper = shallowMount(Navbar);
    expect(wrapper.text()).toContain('Edu');
  })
})

export {}
