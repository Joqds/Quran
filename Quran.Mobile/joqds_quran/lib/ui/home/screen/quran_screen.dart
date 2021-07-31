import 'package:flutter/material.dart';
import 'package:flutter_bloc/flutter_bloc.dart';
import 'package:joqds_quran/bloc/bloc.dart';
import 'package:joqds_quran/ui/read/read_page.dart';

class QuranScreen extends StatefulWidget {
  const QuranScreen({Key? key}) : super(key: key);

  @override
  State<QuranScreen> createState() => _QuranScreenState();
}

class _QuranScreenState extends State<QuranScreen> {
  @override
  Widget build(BuildContext context) {
    return BlocBuilder<SurahBloc, SurahState>(
      builder: (context, state) {
        if (state is UnSurahState) {
          return const Center(child: CircularProgressIndicator());
        }
        if (state is ErrorSurahState) {
          return Center(
              child: Column(
            crossAxisAlignment: CrossAxisAlignment.center,
            children: [
              Text("خطایی رخ داده (${state.errorMessage})"),
              ElevatedButton(
                  onPressed: () {
                    BlocProvider.of<SurahBloc>(context).add(LoadSurahEvent());
                  },
                  child: const Text("تلاش مجدد"))
            ],
          ));
        }
        if (state is InSurahState) {
          return ListView.builder(
            itemCount: state.sovar.length,
            itemBuilder: (context, index) {
              return TextButton(
                onPressed: () => goToRead(state.sovar[index].id!),
                child: Row(
                  mainAxisAlignment: MainAxisAlignment.spaceBetween,
                  children: [
                    Expanded(
                      flex: 1,
                      child: Text(
                        state.sovar[index].id.toString(),
                        textAlign: TextAlign.center,
                        style: Theme.of(context).textTheme.headline5,
                      ),
                    ),
                    Expanded(
                      flex: 5,
                      child: Text(state.sovar[index].name!,
                          textAlign: TextAlign.center,
                          style: Theme.of(context).textTheme.headline5!),
                    ),
                  ],
                ),
              );
            },
          );
        }
        throw Exception();
      },
    );
  }

  goToRead(int surahId) {
    Navigator.push(context, ReadPage(surahId).createRoute(context));
  }
}
