import { Client2Page } from './app.po';

describe('client2 App', () => {
  let page: Client2Page;

  beforeEach(() => {
    page = new Client2Page();
  });

  it('should display welcome message', () => {
    page.navigateTo();
    expect(page.getParagraphText()).toEqual('Welcome to app!!');
  });
});
