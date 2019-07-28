Work in progress


PInvoking into the GetCurrentThreadId is your best bet and will give you the correct information.
However I must warn you, there are very good reasons why the CLR doesn't provide this information: it's almost a completely useless value for managed code. It's perfectly legal from a CLR perspective for a single managed thread to be backed by several different native threads during it's lifetime. This means the result of GetCurrentThreadId can (and will) change throughout the course of a thread's lifetime.
In many applications this is not an observable phenomenon. In a UI application this won't actually happen because it's typically backed by an STA thread which is harder (usually even illegal) to swap out due to COM interop issues. So many developers are blissfully ignorant of this. However it's very easy to swap out MTA threads under the hood which is typically the execution context of a background thread.
